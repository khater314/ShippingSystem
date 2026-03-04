let Alert = {
    ConfirmDelete: function (callBack) {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                callBack(result);
                //Swal.fire({
                //    title: "Deleted!",
                //    text: "Your file has been deleted.",
                //    icon: "success"
                //});
            }
        });
    },
    Success: function (message) {
        Swal.fire({
            icon: "success",
            title: "Success",
            text: message
        });
    },
    Error: function (message) {
        Swal.fire({
            icon: "error",
            title: "Error",
            text: message
        });
    },
}
$(document).ready(function () {
    $(document).on('click', '.js-btn-delete', function (e) {
        e.preventDefault();

        var btn = $(this);
        var id = btn.data('id');
        var deleteUrl = btn.data('url');

        Alert.ConfirmDelete(function (result) {
            if (result) {
                window.location.href = deleteUrl + '/' + id;
            }
        });
    });
});