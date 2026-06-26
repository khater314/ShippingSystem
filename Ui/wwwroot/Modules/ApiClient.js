// ============================================================
//  ApiClient — AJAX wrapper with auth, refresh & retry
// ============================================================
const ApiClient = {

    baseUrl: 'https://localhost:7244',

    /** Default timeout in milliseconds for every request. */
    timeout: 30_000,

    /**
     * Maximum number of times a request is retried after a
     * successful token refresh.  Keeps us from infinite loops.
     */
    _maxRetries: 1,

    /**
     * Holds pending callbacks while a token-refresh is in flight.
     * null  → no refresh running
     * []    → refresh running; callbacks queued here
     */
    _refreshQueue: null,

    // ── Private helpers ──────────────────────────────────────

    _authHeaders(useAuth) {
        if (!useAuth) return {};
        const token = AppHelper.getCookie('AccessToken');
        return token ? { Authorization: `Bearer ${token}` } : {};
    },

    /**
     * Core AJAX dispatcher.
     *
     * @param {object} opts
     * @param {string}   opts.method
     * @param {string}   opts.url          - Path appended to baseUrl
     * @param {*}        [opts.data]       - Payload (object or FormData)
     * @param {function} [opts.onSuccess]
     * @param {function} [opts.onError]
     * @param {boolean}  [opts.useAuth]
     * @param {boolean}  [opts.isFormData] - Skip JSON serialization
     * @param {number}   [opts.retryCount]
     */
    _request({ method, url, data, onSuccess, onError, useAuth = true, isFormData = false, retryCount = 0 }) {
        const ajaxOpts = {
            url: this.baseUrl + url,
            type: method,
            headers: this._authHeaders(useAuth),
            timeout: this.timeout,
            xhrFields: { withCredentials: true },
            success: onSuccess,
            error: (xhr) => this._handleError({ xhr, method, url, data, onSuccess, onError, useAuth, isFormData, retryCount }),
        };

        if (data !== undefined && data !== null) {
            if (isFormData) {
                // Let the browser set Content-Type (with boundary) automatically.
                ajaxOpts.data = data;
                ajaxOpts.processData = false;
                ajaxOpts.contentType = false;
            } else {
                ajaxOpts.data = JSON.stringify(data);
                ajaxOpts.contentType = 'application/json';
            }
        } else {
            ajaxOpts.contentType = 'application/json';
        }

        $.ajax(ajaxOpts);
    },

    /**
     * Centralised error handler.
     * On 401 → refresh token once, then replay the original request.
     * On anything else → call onError with a normalised error object.
     */
    _handleError({ xhr, method, url, data, onSuccess, onError, useAuth, isFormData, retryCount }) {
        const retry = () => {
            if (retryCount < this._maxRetries) {
                this._request({ method, url, data, onSuccess, onError, useAuth, isFormData, retryCount: retryCount + 1 });
            } else {
                this._callOnError(onError, xhr, 'Max retries reached after token refresh.');
            }
        };

        if (useAuth && xhr.status === 401 && retryCount < this._maxRetries) {
            this.refreshToken(retry, (err) => this._callOnError(onError, err, 'Token refresh failed.'));
            return;
        }

        this._callOnError(onError, xhr);
    },

    /**
     * Normalise error info and forward to the caller's onError callback.
     * Always logs to the console so nothing is silently swallowed.
     */
    _callOnError(onError, xhr, fallbackMessage) {
        let message = fallbackMessage || 'An unexpected error occurred.';
        let responseBody = null;

        try {
            responseBody = JSON.parse(xhr.responseText);
            message = responseBody?.message || responseBody?.title || message;
        } catch (_) { /* responseText was not JSON */ }

        const errorInfo = {
            status: xhr.status,
            message,
            response: responseBody,
            raw: xhr,
        };

        console.error('[ApiClient]', errorInfo);

        if (typeof onError === 'function') onError(errorInfo);
    },

    // ── Public API ───────────────────────────────────────────

    /** GET request. */
    get(url, onSuccess, onError, useAuth = true) {
        this._request({ method: 'GET', url, onSuccess, onError, useAuth });
    },

    /** POST request — sends JSON body. */
    post(url, data, onSuccess, onError, useAuth = true) {
        this._request({ method: 'POST', url, data, onSuccess, onError, useAuth });
    },

    /** PUT request — sends JSON body. */
    put(url, data, onSuccess, onError, useAuth = true) {
        this._request({ method: 'PUT', url, data, onSuccess, onError, useAuth });
    },

    /** DELETE request. */
    delete(url, onSuccess, onError, useAuth = true) {
        this._request({ method: 'DELETE', url, onSuccess, onError, useAuth });
    },

    /**
     * Upload files via multipart/form-data.
     *
     * @param {string}    url
     * @param {FormData}  formData   - Build this yourself so you control field names.
     * @param {function}  onSuccess
     * @param {function}  onError
     * @param {boolean}   useAuth
     *
     * @example
     *   const fd = new FormData();
     *   fd.append('file', fileInput.files[0]);
     *   fd.append('description', 'My file');
     *   ApiClient.upload('/api/files', fd, onSuccess, onError);
     */
    upload(url, formData, onSuccess, onError, useAuth = true) {
        this._request({ method: 'POST', url, data: formData, onSuccess, onError, useAuth, isFormData: true });
    },

    /**
     * Refresh the access token.
     *
     * Uses a queue so that if multiple requests fail with 401
     * simultaneously, only ONE refresh call is made.  All callers
     * waiting on the refresh are resolved together afterward.
     */
    refreshToken(onSuccess, onFailure) {
        // If a refresh is already in flight, just queue this callback.
        if (this._refreshQueue !== null) {
            this._refreshQueue.push({ onSuccess, onFailure });
            return;
        }

        // Start the queue.
        this._refreshQueue = [{ onSuccess, onFailure }];

        $.ajax({
            url: this.baseUrl + '/api/auth/refresh-access-token',
            type: 'POST',
            timeout: this.timeout,
            xhrFields: { withCredentials: true },
            contentType: 'application/json',

            success: (response) => {
                if (response?.AccessToken) {
                    AppHelper.setCookie('AccessToken', response.AccessToken, { days: 1 });

                    const queue = this._refreshQueue;
                    this._refreshQueue = null;
                    queue.forEach(({ onSuccess: cb }) => { if (cb) cb(); });
                } else {
                    this._flushRefreshFailure({ message: 'Empty token in refresh response.' });
                }
            },

            error: (xhr) => {
                // 401 / 403 on the refresh endpoint → session is truly dead.
                if (xhr.status === 401 || xhr.status === 403) {
                    AppHelper.removeCookie('AccessToken');
                }
                this._flushRefreshFailure(xhr);
            },
        });
    },

    /** Drain the refresh queue with a failure. */
    _flushRefreshFailure(err) {
        const queue = this._refreshQueue;
        this._refreshQueue = null;
        console.error('[ApiClient] Token refresh failed:', err);
        queue.forEach(({ onFailure: cb }) => { if (cb) cb(err); });
    },
};

/*
const ApiClient = {
    baseUrl: 'https://localhost:7244',

    get: function (url, onSuccess, onError, useAuth = true) {
        const AccessToken = AppHelper.getCookie("AccessToken");
        const headers = useAuth && AccessToken
            ? { 'Authorization': 'Bearer ' + AccessToken }
            : {};

        $.ajax({
            url: this.baseUrl + url,
            type: 'GET',
            contentType: 'application/json',
            headers: headers,
            success: onSuccess,
            error: function (xhr) {
                if (useAuth && xhr.status === 401) {
                    ApiClient.refreshToken(() => {
                        ApiClient.get(url, onSuccess, onError, useAuth);
                    }, onError);
                } else if (onError) {
                    onError(xhr);
                }
            }
        });
    },

    post: function (url, data, onSuccess, onError, useAuth = true) {
        const AccessToken = AppHelper.getCookie("AccessToken");
        const headers = useAuth && AccessToken
            ? { 'Authorization': 'Bearer ' + AccessToken }
            : {};

        $.ajax({
            url: this.baseUrl + url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers,
            xhrFields: {
                withCredentials: true
            },
            success: onSuccess,
            error: function (xhr) {
                if (useAuth && xhr.status === 401) {
                    ApiClient.refreshToken(() => {
                        ApiClient.post(url, data, onSuccess, onError, useAuth);
                    }, onError);
                } else if (onError) {
                    onError(xhr);
                }
            }
        });
    },

    refreshToken: function (onSuccess, onFailure) {


        $.ajax({
            url: this.baseUrl + '/api/auth/RefreshAccessToken',
            type: 'POST',
            contentType: 'application/json',
            xhrFields: {
                withCredentials: true
            },
            //data: JSON.stringify({ refreshToken: refreshToken }),
            success: function (response) {
                if (response && response.AccessToken) {
                    document.cookie = `AccessToken=${response.AccessToken}; path=/`;
                    onSuccess();
                } else {
                    if (onFailure) onFailure({ message: 'Token refresh failed.' });
                }
            },
            error: function (err) {
                if (onFailure) onFailure(err);
            }
        });
    }
};
*/