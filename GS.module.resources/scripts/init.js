//var wsnode = 'http://localhost:50795/contract/'
//var wsnode = 'http://localhost/moduleservices/contract/';
//var wsnode = 'http://192.168.0.103/moduleservices/contract/'
//var wsnode = 'https://intranet.gruposilvestre.com.pe/moduleservices/contract/';


var wsnode = 'http://localhost/moduleservices/contract/';
var pgnode = '/';
var sysname = '';
var argument = '';

function appendScriptChild(url)
{
	$("#headerpages").empty()
	$("#headerpages").append('<script src=\"' + rootInit + url + '\" type="text/javascript"><\/script>');
}

function showSuccess(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.success(message, sysname);

    }, 300);
}

function showError(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.error(message, sysname);

    }, 300);
}

function showWarning(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.warning(message, sysname);

    }, 300);
}

function showSuccessRight(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-right',
            timeOut: 4000
        };
        toastr.success(message, sysname);

    }, 300);
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showPositionError);
    } else {
        showError("Geolocation is not supported by this browser.");
    }
}

function showPosition(position) {
    var latlon = position.coords.latitude + "," + position.coords.longitude;
    showWarning(latlon);
    var img_url = "https://maps.googleapis.com/maps/api/staticmap?center="+ latlon + "&zoom=14&size=500x400&sensor=false&key=AIzaSyBu-916DdpKAjTmJNIgngS6HL_kDIKU0aU";
    showWarning(img_url);
    $("#logo-gs").attr('src', img_url);
    //$("#mapholder").innerHTML = "<img src='" + img_url + "'>";
}

function showPositionError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            showError("User denied the request for Geolocation.");
            break;
        case error.POSITION_UNAVAILABLE:
            showError("Location information is unavailable.");
            break;
        case error.TIMEOUT:
            showError("The request to get user location timed out.");
            break;
        case error.UNKNOWN_ERROR:
            showError("An unknown error occurred.");
            break;
    }
}