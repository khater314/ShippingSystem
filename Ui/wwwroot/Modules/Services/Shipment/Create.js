// $(document).ready(function () {
//     FormValidator.init({
//         formSelector: '#shipmentForm',
//         scrollToError: true
//     });
// });
$('form.steps').on('submit', function (e) {
    e.preventDefault(); // منع الإرسال التقليدي
    alert("i will submit");
    ShipmentService.SaveShipment();
});