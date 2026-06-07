const CityService = {
    _route: '/api/v1/City',

    GetAll: function (onSuccess, onError) {
        ApiClient.get(this._route, onSuccess, onError, false);
    },

    GetById: function (id, onSuccess, onError) {
        if (!id) return ServiceUtils.onInvalidId('CityService.getById', onError);
        ApiClient.get(`${this._route}/${id}`, onSuccess, onError, false);
    },
    GetByCountryId: function (countryId, onSuccess, onError) {
        // if (!countryId) return ServiceUtils.onInvalidId('CityService.getByCountryId', onError);
        ApiClient.get(`${this._route}/country-id/${countryId}`, onSuccess, onError, false);
    }
};