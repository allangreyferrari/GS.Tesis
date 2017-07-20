$(document).ready(function () {
    resizeHTML();
    if (navigator.userAgent.search("Firefox") > -1) {
        $("#logo-gs").attr('src', '../../../moduleresources/images/logo-grupo-silvestre-b.png');
    }
    sysname = 'Seguridad';
    getSession();
    //getLocation();
    $("#btnEntry").click(function () {
        var isValid = true;
        if (isValid) {
            var params = JSON.stringify({
                "key": "kIgh2+HJpss=",
                "parametros": [
                    $("#inUser").val()
                    
                ],
                "cryp": [
                    $("#inContrasenna").val(),
                    session + '|' + $("#inUser").val()
                ]
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
                            localStorage.setItem('session', response.Rows[0].v01);
                            var homeRoot = response.Rows[0].v02.split("@");
                            if (homeRoot.Length == 2)
                                pgnode = homeRoot[1];
                            window.location.replace(homeRoot[0]);
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
    });
});

var session;
function getSession() {
    $("#inUser").focus();
    if (localStorage.getItem('session') == null) {
        $.getJSON(wsnode + "wsCommon.svc/ObtenerSessionID", function (data) {
            session = data;
        });
    }
    else {
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
                        var homeRoot = response.Rows[0].v02.split("@");
                        if (homeRoot.Length==2)
                            pgnode = homeRoot[1];
                        window.location.replace(homeRoot[0]);
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
}

$(window).resize(function () {
    resizeHTML();
});



function resizeHTML() {
    if (parseInt($(window).innerWidth()) < 990) {
        $("#sig-pannel").hide();
        $("#sig-items").hide();
        $("#sig-footer").hide();
    }
    else {
        $("#sig-pannel").show();
        $("#sig-items").show();
        $("#sig-footer").show();
    }
}