const ShipmentService = {

    GetModel: function () {
        const shipmentDto = {
            // Dates dynamically pulled from Pickup step, or using fallback defaults
            ShippingDate: $('#PickupDate').val() ? new Date($('#PickupDate').val()).toISOString() : new Date().toISOString(),
            DeliveryDate: $('#PickupDropoffDate').val() ? new Date($('#PickupDropoffDate').val()).toISOString() : new Date(new Date().setDate(new Date().getDate() + 3)).toISOString(),

            SenderId: "00000000-0000-0000-0000-000000000000", // Will be set by backend or overwritten if updating
            Sender: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                FullName: $('#SenderFullName').val(),
                Email: $('#SenderEmail').val(),
                Phone: $('#SenderPhone').val(),
                CountryId: $('#SenderCountryId').val(), // Added for completeness
                CityId: $('#SenderCityId').val(),
                Address: $('#SenderAddress').val(),
                PostalCode: $('#SenderPostalCode').val(),
                ContactType: parseInt($('#SenderContactType').val()) || 0,
                OtherAddressInfo: $('#ReceiverOtherAddressInfo').val() || "" // Fallback string if not explicitly in form
            },

            ReceiverId: "00000000-0000-0000-0000-000000000000",
            Receiver: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                FullName: $('#ReceiverFullName').val(),
                Email: $('#ReceiverEmail').val(),
                Phone: $('#ReceiverPhone').val(),
                CountryId: $('#ReceiverCountryId').val(), // Added for completeness
                CityId: $('#ReceiverCityId').val(),
                Address: $('#ReceiverAddress').val(),
                PostalCode: $('#ReceiverPostalCode').val(),
                ContactType: 1, // 1 for Receiver only
                OtherAddressInfo: $('#ReceiverOtherAddressInfo').val() || ""
            },

            // Package Details
            PackagingId: $('#PackageType').val() || null,
            Width: parseFloat($('#PackageLength').val()) || 0,   // Mapping to your dimensional controls
            Height: parseFloat($('#PackageHeight').val()) || 0,
            Length: parseFloat($('#PackageLength').val()) || 0,
            Weight: parseFloat($('#PackageWeight').val()) || 0,
            PackageValue: parseFloat($('#PackageDeclaredValue').val()) || 0,

            // Payment & Promo Info
            PaymentMethodName: $('#PaymentCardType').val() || null, // Paypal, Visa, etc.
            PromoCode: $('#ReviewPromoCode').val() || "",
            IsTermsAgreed: $('#ReviewTermsAgreed').is(':checked'),

            // Backend generated / Auditing properties
            ShippingRate: 0.0,
            UserSubscriptionId: null,
            TrackingNumber: null,
            ReferenceId: null
        };

        console.log('Generated shipmentDto:', shipmentDto);
        return shipmentDto;
    },

    GetShipments: function (onSuccess, onError) {
    ApiClient.get(`/api/v1/Shipment/shipments`, onSuccess, onError, true);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/v1/Shipment/${id}`, onSuccess, onError, true);
    },

    SaveShipment: function () {

        var shipmentData = ShipmentService.GetModel();
        console.log('Get Data Model:', shipmentData);

        ApiClient.post(
            `/api/v1/Shipment/Create`,
            shipmentData,
            function (shipmentData) { // Success callback
                console.log('Shipment saved:', shipmentData);
            }, 
            function (xhr) { // Error callback
                console.error('Error saving shipment:', xhr.responseJSON || xhr.statusText);
            });
    }
}
/*
const ShipmentService = {
    FormIds: {},

    GetModel: function () {
        const shipmentDto = {
            ShippingDate: new Date().toISOString(),
            DelivryDate: new Date(new Date().setDate(new Date().getDate() + 3)).toISOString(),

            SenderId: "00000000-0000-0000-0000-000000000000",
            Sender: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                FullName: $('input[name="Sender.FullName"]').val(),
                Email: $('input[name="Sender.Email"]').val(),
                Phone: $('input[name="Sender.Phone"]').val(),
                CityId: $('select[name="Sender.CityId"]').val(),
                Address: $('input[name="Sender.Address"]').val(),
                Contacts: $('input[name="Sender.Contacts"]').val(),
                PostalCode: $('input[name="Sender.PostalCode"]').val(),
                OtherAddressInfo: $('input[name="Sender.OtherAddressInfo"]').val(),
                IsDefaultAddress: $('#Sender_IsDefaultAddress').is(':checked'),
                ContactType: 0   Default: SenderAndReceiver, adjust as needed
            },

            ReceiverId: "00000000-0000-0000-0000-000000000000",
            Receiver: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                FullName: $('input[name="Receiver.FullName"]').val(),
                Email: $('input[name="Receiver.Email"]').val(),
                Phone: $('input[name="Receiver.Phone"]').val(),
                CityId: $('select[name="Receiver.CityId"]').val(),
                Address: $('input[name="Receiver.Address"]').val(),
                Contacts: $('input[name="Receiver.Contacts"]').val(),
                PostalCode: $('input[name="Receiver.PostalCode"]').val(),
                OtherAddressInfo: $('input[name="Receiver.OtherAddressInfo"]').val(),
                ContactType: 0
            },

            ShippingTypeId: $('select[name="ShippingTypeId"]').val(),
            PackagingId: $('select[name="PackagingId"]').val() || null,

            Width: parseFloat($('input[name="Width"]').val()) || 0,
            Height: parseFloat($('input[name="Height"]').val()) || 0,
            Weight: parseFloat($('input[name="Weight"]').val()) || 0,
            Length: parseFloat($('input[name="Length"]').val()) || 0,

            PackageValue: parseFloat($('input[name="PackageValue"]').val()) || 0,
            ShippingRate: 0.0,

            PaymentMethodId: null,
            UserSubscriptionId: null,
            TrackingNumber: null,
            ReferenceId: null
        };
        console.log('shipmentDto:', shipmentDto);
        return shipmentDto;
    },

    FillShipmentForm: function (data) {
        this.FormIds = {
            Id: data.Id,
            SenderId: data.Sender?.Id,
            ReciverId: data.Receiver?.Id,
            TrackingNumber: data.TrackingNumber,
            ShippingRate: data.ShippingRate
        };

        Sender fields
        $('input[name="Sender.FullName"]').val(data.Sender?.FullName || '');
        $('input[name="Sender.Email"]').val(data.Sender?.Email || '');
        $('input[name="Sender.Phone"]').val(data.Sender?.Phone || '');
        $('select[name="Sender.CountryId"]').val(data.Sender?.CountryId || '');
        Assuming ManagePageControls can fetch cities based on country and set city dropdown
        ManagePageControls.fillCityDropdown(
            'select[name="Sender.CityId"]',
            data.Sender?.CountryId,
            data.Sender?.CityId
        );
        $('input[name="Sender.Address"]').val(data.Sender?.Address || '');
        $('input[name="Sender.Contacts"]').val(data.Sender?.Contacts || '');
        $('input[name="Sender.PostalCode"]').val(data.Sender?.PostalCode || '');
        $('input[name="Sender.OtherAddressInfo"]').val(data.Sender?.OtherAddressInfo || '');
        $('#Sender_IsDefaultAddress').prop('checked', data.Sender?.IsDefaultAddress || false);

        Receiver fields
        $('input[name="Receiver.FullName"]').val(data.Receiver?.FullName || '');
        $('input[name="Receiver.Email"]').val(data.Receiver?.Email || '');
        $('input[name="Receiver.Phone"]').val(data.Receiver?.Phone || '');
        $('select[name="Receiver.CountryId"]').val(data.Receiver?.CountryId || '');
        ManagePageControls.fillCityDropdown(
            'select[name="Receiver.CityId"]',
            data.Receiver?.CountryId,
            data.Receiver?.CityId
        );
        $('input[name="Receiver.Address"]').val(data.Receiver?.Address || '');
        $('input[name="Receiver.Contacts"]').val(data.Receiver?.Contacts || '');
        $('input[name="Receiver.PostalCode"]').val(data.Receiver?.PostalCode || '');
        $('input[name="Receiver.OtherAddressInfo"]').val(data.Receiver?.OtherAddressInfo || '');

        Shipment details
        $('select[name="ShippingTypeId"]').val(data.ShippingTypeId || '');
        $('select[name="PackagingId"]').val(data.PackagingId || '');
        $('input[name="Width"]').val(data.Width);
        $('input[name="Height"]').val(data.Height);
        $('input[name="Weight"]').val(data.Weight);
        $('input[name="Length"]').val(data.Length);
        $('input[name="PackageValue"]').val(data.PackageValue);
        $('input[name="TrackingNumber"]').val(data.TrackingNumber ?? '');

        Dates
        if (data.ShippingDate) {
            $('input[name="ShippingDate"]').val(new Date(data.ShippingDate).toISOString().split('T')[0]);
        }
        if (data.DelivryDate) {
            $('input[name="DelivryDate"]').val(new Date(data.DelivryDate).toISOString().split('T')[0]);
        }
    },

    SaveShippment: function () {
        let data = ShipmentService.GetModel();
        console.log('log data before send', data);
        ApiClient.post('/api/Shipment/Create', data,
            function (response) { },
            function (xhr) {
                console.error('API Error:', xhr.responseJSON);
            }
        );
    },

    EditShippment: function () {
        let data = ShipmentService.GetModel();
        data.Id = this.FormIds.Id;
        data.SenderId = this.FormIds.SenderId;
        data.ReceiverId = this.FormIds.ReciverId;
        data.TrackingNumber = this.FormIds.TrackingNumber;
        data.ShippingRate = this.FormIds.ShippingRate;
        console.log('log data before send', data);
        ApiClient.post('/api/Shipment/Edit', data,
            function (response) { },
            function (xhr) {
                console.error('API Error:', xhr.responseJSON);
            }
        );
    },

    GetShipments: function (onSuccess, onError) {
        ApiClient.get(`/api/v1/Shipment/shipments`, onSuccess, onError, true);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/v1/Shipment/${id}`, onSuccess, onError, true);
    },
};
*/