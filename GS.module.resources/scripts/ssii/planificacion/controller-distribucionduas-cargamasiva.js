//$(document).ready(function () {

//});

function IrCargarDistribucionDuas()
{
    argument = null;
    $("#pages").load('../../Content/ssii/planificacion/viewdistribucionduas.html');
}

function ListarDUASCargaMasivaTemp() {

    var dsgridpadre = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.ajax({
                    type: "GET",
                    url: "../../Content/ssii/planificacion/viewdistribucionduasCargamasivaUp.aspx/ListarBasicDua",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    processData: true,
                    cache: false,
                    success: function (response) {
                        options.success(response.d);
                    },
                    error: function (response) { showError(JSON.stringify({ response })); }
                });
            }
        }
    });

    $("#gridpadre").kendoGrid({
        dataSource: dsgridpadre,
        height: 340,
        groupable: false,
        sortable: false,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [10, 20, 'All'],
            buttonCount: 5
        },
        columns: [
        {
            field: "v01",
            title: "Id",
            hidden: true
        },
        {
            field: "v02",
            title: "<center>Número DUA</center>",
            width: 100
        },
        {
            field: "v03",
            title: "Guía",
            width: 120
        },
        {
            field: "v04",
            title: "Volante",
            width: 100
        },
        {
            field: "v05",
            title: "Paquete",
            width: 100
        },
        {
            field: "v06",
            title: "Series",
            width: 60
        },
        {
            field: "v07",
            title: "Bultos",
            width: 60
        }
        ]
    });
}

function ListarDUASCargaMasiva() {
    
    var params = JSON.stringify({
        "key": "PIV8o1/cmq0=",
        "parametros":
            [
                $("#filtroFechaProgramacion").val(),
                localStorage.getItem('session')
            ],
        "cryp": []
    });
    var dsgridpadre = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                $.ajax({
                    type: "POST",
                    url: wsnode + "wsCommon.svc/ListarBasicTwenty",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: params,
                    async: true,
                    processData: true,
                    cache: false,
                    success: function (response) {
                        if (response.Transaction.Type == 1)
                            showError(response.Transaction.Message);
                        if (response.NroRows > 0)
                            dsgridpadre.pageSize(20);
                        options.success(response.Rows);
                    },
                    error: function (err) { showError(response.Transaction.Message); }
                });
            }
        }
    });

    $("#gridpadre").kendoGrid({
        dataSource: dsgridpadre,
        height: 340,
        groupable: false,
        sortable: false,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: [10, 20, 'All'],
            buttonCount: 5
        },
        columns: [
        {
            field: "v01",
            title: "Id",
            hidden: true
        },
        {
            field: "v02",
            title: "<center>Número DUA</center>",
            width: 100
        },
        {
            field: "v03",
            title: "Guía",
            width: 120
        },
        {
            field: "v04",
            title: "Volante",
            width: 100
        },
        {
            field: "v05",
            title: "Paquete",
            width: 100
        },
        {
            field: "v06",
            title: "Series",
            width: 60
        },
        {
            field: "v07",
            title: "Bultos",
            width: 60
        }
        ]
    });
}

function ListarDUASCargaMasivaTemp() {
    var dsgridpadre = new kendo.data.DataSource({
        data: DataEXCEL
    });

    $("#gridpadre").kendoGrid({
        dataSource: dsgridpadre,
        height: 340,
        groupable: false,
        sortable: false,
        selectable: true,
        resizable: true,
        pageable: false,
        /*
        pageable: {
            refresh: true,
            pageSizes: [10, 20, 'All'],
            buttonCount: 5
        },*/
        columns: [
        {
            field: "Fecha",
            title: "<center>Fecha</center>",
            width: 100
        },
        {
            field: "Año",
            title: "<center>Año</center>",
            width: 100
        },
        {
            field: "Dua",
            title: "<center>Número DUA</center>",
            width: 100
        },
        {
            field: "Guia",
            title: "<center>Guía</center>",
            width: 120
        },
        {
            field: "Volante",
            title: "<center>Volante</center>",
            width: 100
        },
        {
            field: "Serie",
            title: "<center>Serie</center>",
            width: 60
        },
        {
            field: "Bultos",
            title: "<center>Bultos</center>",
            width: 60
        },
        {
            field: "Paquete",
            title: "<center>Paquete</center>",
            width: 100
        }
        ]
    });
}

/*
function checkfile(sender) {
    var validExts = new Array(".xls", ".xlsx");
    var fileExt = sender.value;
    console.log(fileExt);
    fileExt = fileExt.substring(fileExt.lastIndexOf('.'));

    if (validExts.indexOf(fileExt) < 0) {
        parent.showError("No ha sido posible procesar el archivo");
        sender.value = "";
        return false;
    }
    else {
        return true;
    }
}
*/