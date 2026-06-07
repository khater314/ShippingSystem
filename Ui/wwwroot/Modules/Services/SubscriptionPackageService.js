const SubscriptionPackageService = {
    _route: '/api/v1/SubscriptionPackage',
    GetAll: function (onSuccess, onError) {
        ApiClient.get(this._route, onSuccess, onError, false);
    },

    GetById: function (id, onSuccess, onError) {
        if (!id) return ServiceUtils.onInvalidId('SubscriptionPackageService.getById', onError);
        ApiClient.get(`${this._route}/${id}`, onSuccess, onError, false);
    }
};