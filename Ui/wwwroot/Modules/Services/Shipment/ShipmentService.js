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
    GetShipmentDetails: function (shipment) {

        if (!shipment) return;

        if (shipment.Sender) {
            $('#ReviewSenderFullName').text(shipment.Sender.FullName || 'N/A');
            $('#ReviewSenderEmail').text(shipment.Sender.Email || 'N/A');
            $('#ReviewSenderPhone').text(shipment.Sender.Phone || 'N/A');
            $('#ReviewSenderAddress').text(shipment.Sender.Address || 'N/A');

            var senderCity = shipment.Sender.City.CityEname || 'N/A';
            $('#ReviewSenderCityName').text(senderCity);

            var senderCountry = shipment.Sender.City.Country.CountryEname || 'N/A';
            $('#ReviewSenderCountryName').text(senderCountry);

            $('#ReviewSenderPostalCode').text(shipment.Sender.PostalCode || 'N/A');
            $('#ReviewSenderOtherAddressInfo').text(shipment.Sender.OtherAddressInfo || 'N/A');
        }

        if (shipment.Receiver) {
            $('#ReviewReceiverFullName').text(shipment.Receiver.FullName || 'N/A');
            $('#ReviewReceiverEmail').text(shipment.Receiver.Email || 'N/A');
            $('#ReviewReceiverPhone').text(shipment.Receiver.Phone || 'N/A');
            $('#ReviewReceiverAddress').text(shipment.Receiver.Address || 'N/A');

            var receiverCity = shipment.Receiver.City.CityEname || 'N/A';
            $('#ReviewReceiverCityName').text(receiverCity);

            var receiverCountry = shipment.Receiver.City.Country.CountryEname || 'N/A';
            $('#ReviewReceiverCountryName').text(receiverCountry);

            $('#ReviewReceiverPostalCode').text(shipment.Receiver.PostalCode || 'N/A');
            $('#ReviewReceiverOtherAddressInfo').text(shipment.Receiver.OtherAddressInfo || 'N/A');
        }

        $('#ReviewWeight').text(shipment.Weight || 0);
        var dims = `${shipment.Length || 0} x ${shipment.Width || 0} x ${shipment.Height || 0} in`;
        $('#ReviewDimensions').text(dims);

        $('#ReviewPackageValue').text(shipment.PackageValue || 0);
        $('#ReviewShippingRate').text(shipment.ShippingRate || 0);

        $('#ReviewShippingDate').text(shipment.ShippingDate ? new Date(shipment.ShippingDate).toLocaleDateString() : 'N/A');
        $('#ReviewDelivryDate').text(shipment.DelivryDate ? new Date(shipment.DelivryDate).toLocaleDateString() : 'N/A');

        $('#ReviewTrackingNumber').text(shipment.TrackingNumber || 'Generated upon confirmation');
    },

    GetShipments: function (onSuccess, onError) {
        ApiClient.get(`/api/v1/Shipment`, onSuccess, onError, true);
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


