$(document).ready(function () {

    DropdownHelper.fillCountryDropdown('#SenderCountryId');
    DropdownHelper.fillCountryDropdown('#ReceiverCountryId');

    DropdownHelper.fillPackgingDropdown('#PackageType');
    DropdownHelper.fillShippingTypesDropdown('#ShippingTypes');

    DropdownHelper.fillCityDropdown('#SenderCityId', '#SenderCountryId');
    DropdownHelper.fillCityDropdown('#ReceiverCityId', '#ReceiverCountryId');

});
$('form.steps').on('submit', function (e) {
    e.preventDefault(); // منع الإرسال التقليدي
    alert("i will submit");
    ShipmentService.SaveShipment();
});