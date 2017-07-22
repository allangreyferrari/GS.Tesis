
$(document).ready(function () {
    $("#hreflogo").append('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
    $.ajax({
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCommon.svc/GetOptions",
        data: null,
        cache: false,
        success: function (data) {
            $("#ul-options").append(data);
        }
    });

    var params = JSON.stringify({
        "key": "0ksP++T9gr8=",
        "parametros": [localStorage.getItem('session')],
        "cryp": []
    });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCommon.svc/ListarBasicTwo",
        dataType: "json",
        data: params,
        async: true,
        processData: false,
        cache: false,
        success: function (response) {
            if (response.Transaction.Type == 0) {
                if (response.Rows.length > 0) {
                    $("#sectionname").append(response.Rows[0].v01);
                }
            }
            else
                showError(response.Transaction.Message);
        },
        error: function (response) {
            showError(response);
            return false;
        }
    });
    if (pgnode != '/')
        OpenPage(pgnode);
    //else
    //    OpenPage("../../Content/seguridad/configuracion/viewSistema.html");
});

function OpenPage(namepage) {    
    $("#pages").load(namepage);
}

function closedsession() {

    var params = JSON.stringify({
        "key": "GdXHt4AGum8=",
        "parametros": [localStorage.getItem('session')],
        "cryp": []
    });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCommon.svc/ListarBasicTwo",
        dataType: "json",
        data: params,
        async: true,
        processData: false,
        cache: false,
        success: function (response) {
            if (response.Transaction.Type == 0) {
                if (response.Rows.length > 0) {
                    localStorage.removeItem('session');
                    pgnode = '/';
                    window.location.replace(response.Rows[0].v01);
                }
            }
            else
                showError(response.Transaction.Message);
        },
        error: function (response) {
            showError(response);
            return false;
        }
    });
}
