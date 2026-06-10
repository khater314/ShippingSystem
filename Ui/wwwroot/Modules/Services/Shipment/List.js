$(document).ready(function () {
    ShipmentService.GetShipments(
        function (shipments) { 
            console.log("Hello from the success fuuuuuuuuuuuuunction")
            console.log("Data: " + shipments)
            console.log(shipments)

            drawShipmentList(shipments);
        },
        function (error) {
            console.error('Error fetching shipments:', error);
        }
    );
});