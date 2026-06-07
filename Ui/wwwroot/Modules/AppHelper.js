// ============================================================
//  AppHelper — utility belt
// ============================================================
const AppHelper = {

    // ── Cookies ─────────────────────────────────────────────

    getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
        return null;
    },

    /**
     * Set a cookie with secure defaults.
     * @param {string} name
     * @param {string} value
     * @param {object} [opts]
     * @param {number}  [opts.days=1]      - Expiry in days (0 = session)
     * @param {string}  [opts.path='/']
     * @param {string}  [opts.sameSite='Strict']  - Strict | Lax | None
     * @param {boolean} [opts.secure=true]
     */
    setCookie(name, value, { days = 1, path = '/', sameSite = 'Strict', secure = true } = {}) {
        let cookie = `${name}=${encodeURIComponent(value)}; path=${path}; SameSite=${sameSite}`;
        if (secure && location.protocol === 'https:') cookie += '; Secure';
        if (days) {
            const expires = new Date(Date.now() + days * 864e5).toUTCString();
            cookie += `; expires=${expires}`;
        }
        document.cookie = cookie;
    },

    removeCookie(name, path = '/') {
        document.cookie = `${name}=; path=${path}; expires=Thu, 01 Jan 1970 00:00:00 GMT`;
    },

    // ── Toasts ───────────────────────────────────────────────

    showToast(message, type = 'info') {
        if (window.toastr) {
            toastr[type](message);
        } else {
            console.warn(`[Toast:${type}]`, message);
            alert(message);
        }
    },

    // ── Dates ────────────────────────────────────────────────

    /**
     * Format a date value to a locale-aware string.
     * @param {string|Date|number} date
     * @param {string} [locale]   - e.g. 'en-US'. Defaults to browser locale.
     * @param {object} [options]  - Intl.DateTimeFormat options
     */
    formatDate(date, locale, options = { dateStyle: 'medium', timeStyle: 'short' }) {
        const d = date instanceof Date ? date : new Date(date);
        if (isNaN(d)) return '—';
        return new Intl.DateTimeFormat(locale, options).format(d);
    },

    /** Return UTC ISO string — useful when sending dates to APIs. */
    toISOString(date) {
        const d = date instanceof Date ? date : new Date(date);
        return isNaN(d) ? null : d.toISOString();
    },

    // ── URL helpers ──────────────────────────────────────────

    getQueryParam(name) {
        return new URLSearchParams(window.location.search).get(name);
    },

    /**
     * Get the last path segment — e.g. /users/42 → "42".
     * Returns null when the path ends with a slash.
     */
    getIdFromPath() {
        const segments = window.location.pathname.split('/').filter(Boolean);
        return segments.length ? segments[segments.length - 1] : null;
    },

    // ── Misc ─────────────────────────────────────────────────

    /** Simple deep-clone using JSON (good for plain objects/arrays). */
    deepClone(obj) {
        return JSON.parse(JSON.stringify(obj));
    },

    getCurrentLang() {
        return document.documentElement.lang?.substring(0, 2).toLowerCase() || 'en';
    }
};

/*
const AppHelper = {
    getCookie: function (name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
        return null;
    },

    showToast: function (message, type = 'info') {
        if (window.toastr) {
            toastr[type](message);
        } else {
            alert(message);
        }
    },

    formatDate: function (date) {
        return new Date(date).toISOString();
    },

    getQueryParam: function (name) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(name);
    },

    getIdFromPath: function () {
        const segments = window.location.pathname.split('/');
        return segments[segments.length - 1]; 
    }
};
*/
