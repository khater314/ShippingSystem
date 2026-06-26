
$(document).ready(function () {
    // 1. لقط الـ ID من الـ Query String
    const url = window.location.pathname
    const shipmentId = url.split('/').pop();

    console.log("URL: ", url);
    console.log("ID: ", shipmentId);
    ShipmentService.GetById(
        shipmentId,
        function (details) {
            console.log("Successed.");
            console.log("Details: ", details);
            ShipmentService.GetShipmentDetails(details.Data);
        },
        function (error) {
            console.error("Show Error");
            console.error("error: ", error);
        }
    );
});