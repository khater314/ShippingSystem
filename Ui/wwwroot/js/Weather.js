function getCookie(name) {
    const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    return match ? match[2] : null;
}

function GetWeather() {
    debugger;
    const accessToken = getCookie('AccessToken');

    if (!accessToken) {
        alert('Access token not found. Please log in to access the weather data.');
        //return;
    }
    debugger;
    $.ajax({
        url: "https://localhost:7244/WeatherForecast/",
        type: "GET",
        //dataType: "json",
        xhrFields: {
            withCredentials: true
        },
        headers: {
            "Authorization": "Bearer " + accessToken
        },
        success: function (data) {
            debugger;
            console.log("The Weather Data: ", data);
            $("#api-result").html(DrowHtml(data));
        },
        error: function (xhr) {
            debugger;
            //console.log("error: " + data);
            if (xhr.status === 401) {
                refreshAccessToken(function (success) {
                    console.log("Access token refresh callback: ", success);
                    if (success) { GetWeather(); }
                    else { alert('Access token refresh failed. Please log in again.'); }
                });
            }
        }
    });
            
}
function refreshAccessToken(callback) {
    $.ajax({
        url: 'https://localhost:7244/api/auth/refresh-access-token', // اتأكد من البورت بتاعك
        type: 'POST',
        xhrFields: {
            withCredentials: true // مهم جداً عشان يبعت الـ Refresh Token القديم
        },
        success: function (data) {
            // لو الـ API بعت توكن جديد فعلاً
            console.log("refreshAccessToken/success:data= " + data)
            if (data.AccessToken) {
                console.log("refreshAccessToken/success(if):data.AccessToken= " + data.AccessToken)
                // بنحفظ التوكن الجديد في الكوكيز
                document.cookie = `AccessToken=${data.AccessToken}; path=/`;
                console.log("Access token refreshed successfully");
                callback(true);
            } else {
                console.log("refreshAccessToken/success(else)");
                callback(false);
            }
        },
        error: function () {
            console.error("Access token refreshed failed");
            callback(false);
        }
    });
}
function DrowHtml(items) {
    var table =
        `
                    <table class='table'>
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>TemperatureC</th>
                                <th>TemperatureF</th>
                                <th>Summary</th>
                            </tr>
                        </thead>
                        <tbody>
                    `;

    var endTable = "</tbody></table>";

    var itemHtml = "";

    for (var i = 0; i < items.length; i++) {
        itemHtml =
            `
                            <tr>
                                <td>${items[i].date}</td>
                                <td>${items[i].temperatureC}</td>
                                <td>${items[i].temperatureF}</td>
                                <td>${items[i].summary}</td>
                            </tr>
                        `;
        table += itemHtml;
    }
    table += endTable;
    return table;
}


GetWeather();
//var AjaxWeather = {
//    GetWeather: function () {
//        Helper.AjaxCallGet("https://localhost:7244/WeatherForecast/", null, "json",
//            function (data) {
//                console.log("hahahahahah");
//                console.log("The Fucking Data: ", data);
//                var html = AjaxWeather.DrowHtml(data);
//                $("#api-result").html(html);
//            });
//    },
//    DrowHtml: function (items) {
//        var table =
//        `
//        <table class='table'>
//            <thead>
//                <tr>
//                    <th>Date</th>
//                    <th>TemperatureC</th>
//                    <th>TemperatureF</th>
//                    <th>Summary</th>
//                </tr>
//            </thead>
//            <tbody>
//        `;

//        var endTable = "</tbody></table>";

//        var itemHtml = "";

//        for (var i = 0; i < items.length; i++)
//        {
//            itemHtml =
//            `
//                <tr>
//                    <td>${items[i].date}</td>
//                    <td>${items[i].temperatureC}</td>
//                    <td>${items[i].temperatureF}</td>
//                    <td>${items[i].summary}</td>
//                </tr>
//            `;
//            table += itemHtml;
//        }
//        table += endTable;
//        return table;
//    }
//}

////$(document).ready(function () {
////    AjaxWeather.GetWeather();
////});   
