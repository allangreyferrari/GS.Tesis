$(document).ready(function () {
    var params = JSON.stringify({
        "key": "2X8mqRKCUcY=",
        "parametros":
            [
                localStorage.getItem('session')
            ],
        "cryp": []
    });

    $.ajax({
        type: "POST",
        url: wsnode + "wsCommon.svc/ListarBasicFive",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: true,
        processData: true,
        cache: false,
        success: function (response) {
            console.log(response);

            if (response.Transaction.Type == 1)
                showError(response.Transaction.Message);
            else if (response.NroRows = 0)
                showError("Ocurrio un error al interntar recuperar el registro");
            else {
                var observable = kendo.observable({
                    source: new kendo.data.DataSource({
                        data: JSON.parse(response.Rows[0].v01)
                    })
                });
                kendo.bind(document.body, observable);
            }
        },
        error: function (response) {
        }
    });    
});

function Regresar() {
    argument = null;
    $("#pages").load('../../Content/ssii/ejecucion/viewinspeccionesfisicas.html');
}


function xmlToJson(xml) {
    var obj = {};

    if (xml.nodeType == 1) { 
        if (xml.attributes.length > 0) {
            obj["@attributes"] = {};
            for (var j = 0; j < xml.attributes.length; j++) {
                var attribute = xml.attributes.item(j);
                obj["@attributes"][attribute.nodeName] = attribute.nodeValue;
            }
        }
    } else if (xml.nodeType == 3) {
        obj = xml.nodeValue;
    }

    if (xml.hasChildNodes() && xml.childNodes.length === 1 && xml.childNodes[0].nodeType === 3) {
        obj = xml.childNodes[0].nodeValue;
    }
    else if (xml.hasChildNodes()) {
        for (var i = 0; i < xml.childNodes.length; i++) {
            var item = xml.childNodes.item(i);
            var nodeName = item.nodeName;
            if (typeof (obj[nodeName]) == "undefined") {
                obj[nodeName] = xmlToJson(item);
            } else {
                if (typeof (obj[nodeName].push) == "undefined") {
                    var old = obj[nodeName];
                    obj[nodeName] = [];
                    obj[nodeName].push(old);
                }
                obj[nodeName].push(xmlToJson(item));
            }
        }
    }
    return obj;
}