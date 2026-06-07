$(document).ready(function () {
    DropdownHelper.fillCountryDropdown('#SenderCountryId');
    DropdownHelper.fillCountryDropdown('#ReceiverCountryId');

    DropdownHelper.fillPackgingDropdown('#PackageType');
    DropdownHelper.fillShippingTypesDropdown('#ShippingTypes');

    DropdownHelper.fillCityDropdown('#SenderCityId', '#SenderCountryId');
    DropdownHelper.fillCityDropdown('#ReceiverCityId', '#ReceiverCountryId');

});

/*
$(document).ready(function () {
    // تعبئة الدول
    ManagePageControls.fillCountryDropdown('select[name="Sender.CountryId"]');
    ManagePageControls.fillCountryDropdown('select[name="Receiver.CountryId"]');

    // تعبئة أنواع الشحن
    ManagePageControls.fillShippingTypesDropdown('select[name="ShippingTypeId"]');

    // تعبئة أنواع التغليف
    ManagePageControls.fillShippingPackgingDropdown('select[name="PackagingId"]');

    // تعبئة المدن بناءً على البلد
    $('select[name="Sender.CountryId"]').on('change', function () {
        const countryId = $(this).val();
        ManagePageControls.fillCityDropdown('select[name="Sender.CityId"]', countryId, null);
    });

    $('select[name="Receiver.CountryId"]').on('change', function () {
        const countryId = $(this).val();
        ManagePageControls.fillCityDropdown('select[name="Receiver.CityId"]', countryId, null);
    });
});
*/