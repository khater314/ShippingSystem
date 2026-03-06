var res = window.Resources;
let Alert = {
    ConfirmDelete: function (callBack) {
        Swal.fire({
            title: res.confirm,
            text: res.msg_warning,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: window.Resources.yes,
            cancelButtonText: window.Resources.cancel,
        }).then((result) => {
            if (result.isConfirmed) {
                callBack(result);
            }
        });
    },

    Success: function (title, message) {
        Swal.fire({
            icon: "success",
            title: title,
            text: message
        });
    },
    Error: function (title, message) {
        Swal.fire({
            icon: "error",
            title: title,
            text: message
        });
    },
    Warning: function (title, message) {
        Swal.fire({
            icon: "warning",
            title: title,
            text: message
        });
    },
    Info: function (title, message) {
        Swal.fire({
            icon: "info",
            title: title,
            text: message
        });
    },
    Question: function (title, message) {
        Swal.fire({
            icon: "question",
            title: title,
            text: message
        });
    },

    ShowFromTempData: function (type, body) {
        if (!type || !body) return;

        const icon = type.toLowerCase().includes("success") ? "success" : "error";
        const title = type.toLowerCase().includes("success") ? window.Resources.success : window.Resources.warning;

        if (icon === "success") this.Success(title, body);
        else this.Error(title, body);
    }
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
document.addEventListener("DOMContentLoaded", function () {

    const msgType = window.TempDataMessage.type;
    const msgBody = window.TempDataMessage.body;

    if (msgType && msgBody) {
        Alert.ShowFromTempData(msgType, msgBody);
    }
});

