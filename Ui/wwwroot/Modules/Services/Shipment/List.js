function drawShipmentList(apiResponse) {
    const $tbody = $('#shipmentTableBody');
    $tbody.empty(); // Clear out existing rows or loading spinners

    // 1. Handle API Failures using your backend properties safely
    if (!apiResponse || apiResponse.IsSuccess === false) {
        const errorMsg = apiResponse?.Message || "Failed to load shipment records.";
        $tbody.append(`<tr><td colspan="7" class="text-center text-danger">${errorMsg}</td></tr>`);
        return;
    }

    // 2. Unwrap the collection array from your generic T Data container
    const shipments = apiResponse.Data;

    // 3. Handle Empty Data State gracefully
    if (!shipments || shipments.length === 0) {
        $tbody.append('<tr><td colspan="7" class="text-center text-muted">No shipments found.</td></tr>');
        return;
    }

    // Date formatting helper ("dd MMMM yyyy")
    const formatDate = (dateString) => {
        if (!dateString) return 'N/A';
        const date = new Date(dateString);
        return date.toLocaleDateString('en-GB', {
            day: '2-digit',
            month: 'long',
            year: 'numeric'
        });
    };

    // 4. Render the data loop using jQuery
    $.each(shipments, function (index, shipment) {
        const senderName = shipment.Sender?.FullName || 'N/A';
        const receiverName = shipment.Receiver?.FullName || 'N/A';
        const packageValue = shipment.PackageValue ? Number(shipment.PackageValue).toFixed(2) : '0.00';
        const formattedDate = formatDate(shipment.DelivryDate || shipment.DeliveryDate);

        const rowHTML = `
            <tr>
                <td>${index + 1}</td>
                <td>${shipment.TrackingNumber || 'N/A'}</td>
                <td>${formattedDate}</td>
                <td>${senderName}</td>
                <td>${receiverName}</td>
                <td>$${packageValue}</td>
                <td style="display:flex; justify-content:space-around;">
                    <a href="/Shipment/Delete/${shipment.Id}" title="Delete">
                        <i class="far fa-trash-alt text-danger"></i>
                    </a>
                    <a href="/Shipment/Check/${shipment.Id}" title="Verify">
                        <i class="fa fa-check-circle text-success" aria-hidden="true"></i>
                    </a>
                    <a href="/Shipment/Details/${shipment.Id}" title="Details">
                        <i class="fa fa-info-circle text-info"></i>
                    </a>
                </td>
            </tr>
        `;

        $tbody.append(rowHTML);
    });
}

$(document).ready(function () {
    ShipmentService.GetShipments(
        function (shipments) { 

            drawShipmentList(shipments);
        },
        function (error) {
            console.error('Error fetching shipments:', error);
        }
    );
});
