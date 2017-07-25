$(document).ready(function () {
    $("#filtroFecha").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $('#filtroFecha').datepicker({
        format: "dd/mm/yyyy",
        language: "es",
        calendarWeeks: true,
        multidateSeparator: "/",
        beforeShowYear: function (date) {
            if (date.getFullYear() == 2007) {
                return false;
            }
        }
    });

    SetToday("filtroFecha");

    ListarInspeccionesFiltro();
});

function SetToday(inputDATE) {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    var today = dd + '/' + mm + '/' + yyyy;
    $("#" + inputDATE).val(today);
}


function Relacionar() {

    var params = JSON.stringify({
        "key": "bFHXyZa3Tmk=",
        "parametros":
            [
                localStorage.getItem('session')
            ],
        "cryp": []
    });

    $.ajax({
        type: "POST",
        url: wsnode + "wsCommon.svc/ListarBasicTen",
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
                showError("Ud. ya tiene asignado un paquete para el día");
            else {
                $("#modalRelacionarHeader").html("<i class=\"fa fa-handshake-o\">&nbsp;</i> Relacionar");
                $("#modalRelacionar").modal("show");

                $("#modalRelacionarFecha").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
                $('#modalRelacionarFecha').datepicker({
                    format: "dd/mm/yyyy",
                    language: "es",
                    calendarWeeks: true,
                    multidateSeparator: "/",
                    beforeShowYear: function (date) {
                        if (date.getFullYear() == 2007) {
                            return false;
                        }
                    }
                });

                SetToday("modalRelacionarFecha");

                $("#gridrelacionar").empty();
                $("#modalRelacionarPaquete").val("");
            }
        },
        error: function (response) {
        }
    });
}

function keypress(e) {
    if (e.keyCode === 13) {
        e.preventDefault();

        $("#gridrelacionar").empty();

        var params = JSON.stringify({
            "key": "BQoZCyuSGbA=",
            "parametros":
                [
                    $('#modalRelacionarFecha').val(),
                    $('#modalRelacionarPaquete').val(),
                    localStorage.getItem('session')
                ],
            "cryp": []
        });

        var dsgrid = new kendo.data.TreeListDataSource({
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCommon.svc/ListarBasicTen",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: true,
                        processData: true,
                        cache: false,
                        success: function (response) {
                            if (response.Transaction.Type == 1)
                                showError(response.Transaction.Message);                            
                            if (response.NroRows > 0) {
                                dsgrid.pageSize(20);
                                setTimeout(options.success(response.Rows), 2000);
                            } else {
                                showError("No existen DUAs disponibles para el paquete ingresado");                                
                            }

                        },
                        error: function (err) { }
                    });
                }
            }
        });

        $("#gridrelacionar").kendoGrid({
            dataSource: dsgrid,
            height: 300,

            rowTemplate:
                        "<tr>" +
                        "<td><center>#: v02 #</center></td>" +
                        "<td><center>#: v03 #</center></td>" +
                        "<td><center>#: v04 #</center></td>" +
                        "<td><center>#: v05 #</center></td>" +
                        "<td><center>#: v06 #</center></td>" +
                        "</tr>",
            altRowTemplate:
                        "<tr>" +                        
                        "<td><center>#: v02 #</center></td>" +
                        "<td><center>#: v03 #</center></td>" +
                        "<td><center>#: v04 #</center></td>" +
                        "<td><center>#: v05 #</center></td>" +
                        "<td><center>#: v06 #</center></td>" +
                        "</tr>",
            group: {
                field: "saludo"
            },
            groupable: false,
            sortable: true,
            selectable: true,
            resizable: true,
            columns: [
            {
                field: "v02",
                title: "<center>Número DUA</center>",
                width: 80
            },
            {
                field: "v03",
                title: "<center>Guía</center>",
                width: 80
            },
            {
                field: "v04",
                title: "<center>Paquete</center>",
                width: 80
            },
            {
                field: "v05",
                title: "<center>Cantidad de<br>Series</center>",
                width: 80
            },
            {
                field: "v06",
                title: "<center>Bultos</center>",
                width: 50
            }
            ]
        });
    }
}

function GuardarRelacion() {

    var params = JSON.stringify({
        "key": "iUeaL4yLC3g=",
        "parametros":
            [
                $('#modalRelacionarFecha').val(),
                $('#modalRelacionarPaquete').val(),
                localStorage.getItem('session')
            ],
        "cryp": []
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCommon.svc/EjecutarTransaction",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: true,
        processData: false,
        cache: false,
        success: function (transaction) {
            if (transaction.Type == 1)
                showSuccess(transaction.Message);
            else
                showError(transaction.Message);
            $("#modalRelacionar").modal("hide");
            ListarInspeccionesFiltro();
        },
        error: function (transaction) {
            showError(JSON.stringify(transaction.Message));
        }
    });
    $("#modalRelacionar").modal("hide");
}

function ListarInspeccionesFiltro() {
    $("#gridpadre").empty();
    var params = JSON.stringify({
        "key": "U2uOI7WhOsw=",
        "parametros":
            [
                $('#filtroFecha').val(),
                $('#filtroPaquete').val(),
                $('#filtroDua').val(),
                $('#filtroVolante').val(),
                localStorage.getItem('session')
            ],
        "cryp": []
    });

    var dsgridpadre = new kendo.data.TreeListDataSource({
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
                        if (response.NroRows > 0) {
                            dsgridpadre.pageSize(20);
                            setTimeout(options.success(response.Rows), 2000);
                        }

                    },
                    error: function (err) { }
                });
            }
        }
    });

    $("#gridpadre").kendoGrid({
        dataSource: dsgridpadre,
        //dataSource: [
        //    { saludo: "Jane Doe", age: 30 },
        //    { saludo: "John Doe", age: 33 }
        //],
        height: 400,

        rowTemplate:
                    "<tr>" +
                    "<td><div style='cursor:pointer'><center>" +
                        "<i title='Ver detalle' class=\"fa fa-eye\" onclick=\"VerDetalle('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Iniciar llamado' class=\"fa fa-clock-o\" onclick=\"IniciarLlamado('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Revisión documentaria' class=\"fa fa-folder-open-o\" onclick=\"RevisionDocumentaria('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Revisión física' class=\"fa fa-cube\" onclick=\"RevisionFisica('#: v01 #'); return false;\">&nbsp;</i>" +
                        "</center></div></td>" +
                    "<td><center>#: v02 #</center></td>" +
                    "<td><center>#: v03 #</center></td>" +
                    "<td><center>#: v04 #</center></td>" +
                    "<td><center>#: v05 #</center></td>" +
                    "<td><center>#: v06 #</center></td>" +
                    "<td><center>#: v07 #</center></td>" +
                    "<td><center>#: v08 #</center></td>" +
                    "<td><center>#: v09 #</center></td>" +
                    "</tr>",
        altRowTemplate:
                    "<tr>" +
                    "<td><div style='cursor:pointer'><center>"+
                        "<i title='Ver detalle' class=\"fa fa-eye\" onclick=\"VerDetalle('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Iniciar llamado' class=\"fa fa-clock-o\" onclick=\"IniciarLlamado('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Revisión documentaria' class=\"fa fa-folder-open-o\" onclick=\"RevisionDocumentaria('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Revisión física' class=\"fa fa-cube\" onclick=\"RevisionFisica('#: v01 #'); return false;\">&nbsp;</i>" +
                        "</center></div></td>" +
                    "<td><center>#: v02 #</center></td>" +
                    "<td><center>#: v03 #</center></td>" +
                    "<td><center>#: v04 #</center></td>" +
                    "<td><center>#: v05 #</center></td>" +
                    "<td><center>#: v06 #</center></td>" +
                    "<td><center>#: v07 #</center></td>" +
                    "<td><center>#: v08 #</center></td>" +
                    "<td><center>#: v09 #</center></td>" +
                    "</tr>",
        group: {
            field: "saludo"
        },
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
            template: "<div style='cursor:pointer'><center>" +
                        "<i title='Ver detalle' class=\"fa fa-eye\" onclick=\"VerDetalle('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Iniciar llamado' class=\"fa fa-clock-o\" onclick=\"IniciarLlamado('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Revisión documentaria' class=\"fa fa-folder-open-o\" onclick=\"RevisionDocumentaria('#: v01 #'); return false;\">&nbsp;</i>" +
                        "<i title='Revisión física' class=\"fa fa-cube\" onclick=\"RevisionFisica('#: v01 #'); return false;\">&nbsp;</i>" +
                        "</center></div></td>",
            title: "",
            width: 60
        },
        {
            field: "v02",
            title: "<center>Número DUA</center>",
            width: 100
        },
        {
            field: "v03",
            title: "<center>Volante</center>",
            width: 100
        },
        {
            field: "v04",
            title: "<center>Turno de<br>Atención</center>",
            width: 100
        },
        {
            field: "v05",
            title: "<center>Cantidad de<br>Series</center>",
            width: 50
        },
        {
            field: "v06",
            title: "<center>Bultos</center>",
            width: 50
        },
        {
            field: "v07",
            title: "<center>Fecha de<br>Turno</center>",
            width: 100
        },
        {
            field: "v08",
            title: "<center>Clasificación de<br>Horario</center>",
            width: 100
        },
        {
            field: "v09",
            title: "<center>Estado de<br>turno</center>",
            width: 100
        }
        ]
    });
}

function VerDetalle(id) {
    var params = JSON.stringify({
        "key": "uLxG8DprxXg=",
        "parametros":
            [
                parseInt(id),
                localStorage.getItem('session')
            ],
        "cryp": []
    });

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
            if (response.NroRows = 0)
                showError("Ocurrio un error al interntar recuperar el registro");

            $("#modalDUAHeader").html("<i class=\"fa fa-plus\">&nbsp;</i> Modificar DUA");
            $("#modalDetalleId").val(response.Rows[0].v01);

            $("#modalDetalleGuia").val(response.Rows[0].v02)
            $("#modalDetalleVolante").val(response.Rows[0].v03);
            $("#modalDetalleBultos").val(response.Rows[0].v04);
            $("#modalDetalleKilos").val(response.Rows[0].v05);
            $("#modalDetalleAgente").val(response.Rows[0].v06);
            $("#modalDetalleLineaAerea").val(response.Rows[0].v07);
            $("#modalDetalleCliente").val(response.Rows[0].v08);
            $("#modalDetalleDescripcion").val(response.Rows[0].v09);
            $("#modalDetalleUbicacion").val(response.Rows[0].v10);

            $("#modalDetalleDua").val(response.Rows[0].v11);
            $("#modalDetalleTurno").val(response.Rows[0].v12);
            $("#modalDetalleFechaAtencion").val(response.Rows[0].v13);
            $("#modalDetalleHorario").val(response.Rows[0].v14);

            $("#modalDetalleHeader").html("<i class=\"fa fa-eye\">&nbsp;</i> Detalle");
            $("#modalDetalle").modal("show");

        },
        error: function (response) {
        }
    });
}

function RevisionDocumentaria(id) {
    if (InspeccionAsignadaAUsuario(id)) {

        $("#modalRevisionHeader").html("<i class=\"fa fa-folder-open-o\">&nbsp;</i> Revisión documentaria");
        $("#modalRevision").modal("show");

        $("#gridRevisionDocumentaria").kendoGrid({
            dataSource: [
                { saludo: "Jane Doe", age: 30 },
                { saludo: "John Doe", age: 33 }
            ],
            height: 200,

            rowTemplate:
                        "<tr>" +
                        "<td><center>#: saludo #</center></td>" +
                        //"<td><center>#: v02 #</center></td>" +
                        //"<td><center>#: v03 #</center></td>" +
                        //"<td><center>#: v04 #</center></td>" +
                        "</tr>",
            altRowTemplate:
                        "<tr>" +
                        "<td><center>#: saludo #</center></td>" +
                        //"<td><center>#: v02 #</center></td>" +
                        //"<td><center>#: v03 #</center></td>" +
                        //"<td><center>#: v04 #</center></td>" +                  
                        "</tr>",
            group: {
                field: "saludo"
            },
            groupable: false,
            sortable: true,
            selectable: true,
            resizable: true,
            columns: [
            {
                field: "v02",
                title: "<center>Nombre</center>",
                width: 100
            },
            {
                field: "v03",
                title: "<center>Fecha de carga</center>",
                width: 70
            },
            {
                field: "v04",
                title: "<center>Estado</center>",
                width: 70
            },
            {
                field: "v05",
                title: "<center>Ver</center>",
                width: 70
            }
            ]
        });
    } else {
        showError("Esta inspección ha sido asignada a otro usuario");
    }
}

function RevisionFisica() {
    if (InspeccionAsignadaAUsuario(id)) {
        $("#modalRevisionHeader").html("<i class=\"fa fa-cube\">&nbsp;</i> Revisión física");
        $("#modalRevision").modal("show");

        $("#gridRevisionDocumentaria").kendoGrid({
            dataSource: [
                { saludo: "Jane Doe", age: 30 },
                { saludo: "John Doe", age: 33 }
            ],
            height: 200,

            rowTemplate:
                        "<tr>" +
                        "<td><center>#: saludo #</center></td>" +
                        //"<td><center>#: v02 #</center></td>" +
                        //"<td><center>#: v03 #</center></td>" +
                        //"<td><center>#: v04 #</center></td>" +
                        "</tr>",
            altRowTemplate:
                        "<tr>" +
                        "<td><center>#: saludo #</center></td>" +
                        //"<td><center>#: v02 #</center></td>" +
                        //"<td><center>#: v03 #</center></td>" +
                        //"<td><center>#: v04 #</center></td>" +                  
                        "</tr>",
            group: {
                field: "saludo"
            },
            groupable: false,
            sortable: true,
            selectable: true,
            resizable: true,
            columns: [
            {
                field: "v02",
                title: "<center>Nombre</center>",
                width: 100
            },
            {
                field: "v03",
                title: "<center>Fecha de carga</center>",
                width: 70
            },
            {
                field: "v04",
                title: "<center>Estado</center>",
                width: 70
            },
            {
                field: "v05",
                title: "<center>Ver</center>",
                width: 70
            }
            ]
        });
    } else {
        showError("Esta inspección ha sido asignada a otro usuario");
    }
}

function InspeccionAsignadaAUsuario(id) {

    var asignado = false;

    var params = JSON.stringify({
        "key": "apqv+NAK2Qo=",
        "parametros":
            [
                parseInt(id),
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
            showError(response.NroRows);
            if (response.Transaction.Type == 1)
                showError(response.Transaction.Message);
            if (response.NroRows > 0)
                asignado = true;
        },
        error: function (response) {            
        }
    });

    return asignado;
}