$(document).ready(function () {
    $("#filtroFechaIni").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $('#filtroFechaIni').datepicker({
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

    $("#filtroFechaFin").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $('#filtroFechaFin').datepicker({
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

    $("#modalDUAFechaAtencion").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $('#modalDUAFechaAtencion').datepicker({
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

    SetToday("filtroFechaIni");
    SetToday("filtroFechaFin");

    $('#modalDUA').on('shown.bs.modal', function () {
        $(document).off('focusin.modal');
    });

    ListarDUASFiltro();
});

function BuscarVolante()
{
    showError("No se pudo acceder a la informacion del Volante y la Guia");
}

function CambiarEstado() {
    if ($("#modalDUAboolActivo").val() == "true") {
        $("#modalDUAActivo").attr("src", "../../../moduleresources/images/uncheck.png");
        $("#modalDUAboolActivo").val("false");
    }
    else {
        $("#modalDUAActivo").attr("src", "../../../moduleresources/images/check.png");
        $("#modalDUAboolActivo").val("true");
    }
}

function GuardarPopup() {

    var params = JSON.stringify({
        "key": "z2hqGChwPl8=",
        "parametros":
            [
                parseInt($("#modalDUAId").val()),
                $("#modalDUAAnio").val(),
                $("#modalDUANumero").val(),
                $("#modalDUAGuia").val(),
                $("#modalDUAVolante").val(),
                
                parseInt($("#modalDUASerie").val()),
                parseInt($("#modalDUABultos").val()),
                $("#modalDUAPaquete").val(),

                $("#modalDUAFechaAtencion").val(),
                $("#PopupOpcionboolEstado").val(),
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
            $("#modalDUA").modal("hide");
            ListarDUASFiltro();
        },
        error: function (transaction) {
            showError(JSON.stringify(transaction.Message));
        }
    });
    $("#modalDUA").modal("hide");
}

function CancelarPopup() {
    $("#modalDUA").modal("hide");
}

function Nuevo()
{
    $("#modalDUAHeader").html("<i class=\"fa fa-plus\">&nbsp;</i> Nueva DUA");
    $("#modalDUAId").val("0");
    var fecha = new Date();
    var anio = fecha.getFullYear();
    $("#modalDUAAnio").val(anio);
    $("#modalDUANumero").mask('9999');
    $("#modalDUANumero").val("");
    $("#modalDUANumero").mask('999999');
    $("#modalDUAGuia").val("");
    $("#modalDUAGuia").mask('999-99999999');
    $("#modalDUAVolante").val("");
    $("#modalDUAVolante").mask('99999999');
    $("#modalDUASerie").val("");
    $("#modalDUASerie").mask('9999');
    $("#modalDUABultos").val("");
    $("#modalDUABultos").mask('9999');
    $("#modalDUAPaquete").val("PAQ");
    $("#modalDUAPaquete").mask('PAQ999');
    $("#modalDUAFechaAtencion").val("");

    $("#modalDUAGuia").prop("disabled", false);
    $("#modalDUAVolante").prop("disabled", false);

    $("#modalDUAActivo").attr("src", "../../../moduleresources/images/uncheck.png");
    $("#modalDUAboolActivo").val("true");

    $("#modalDUA").modal("show");

    SetToday("modalDUAFechaAtencion");
}

function Modificar(id)
{
    var params = JSON.stringify({
        "key": "OEsAllGD/0A=",
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
            $("#modalDUAId").val(response.Rows[0].v01);

            $("#modalDUAAnio").val(response.Rows[0].v02);
            $("#modalDUANumero").val(response.Rows[0].v03);
            $("#modalDUAGuia").val(response.Rows[0].v04);
            $("#modalDUAGuia").prop("disabled", true);
            $("#modalDUAVolante").val(response.Rows[0].v05);
            $("#modalDUAVolante").prop("disabled", true);
            $("#modalDUASerie").val(response.Rows[0].v06);
            $("#modalDUABultos").val(response.Rows[0].v07);
            $("#modalDUAPaquete").val(response.Rows[0].v08);
            $("#modalDUAFechaAtencion").val(response.Rows[0].v09);
            console.log(response.Rows[0].v10);

            $("#modalDUAActivo").attr("src", response.Rows[0].v11);
            $("#modalDUAboolActivo").val(response.Rows[0].v10);

            $("#modalDUA").modal("show");

        },
        error: function (response) {
        }
    });
}

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

function IrCargaMasiva()
{
    argument = null;
    $("#pages").load('../../Content/ssii/planificacion/viewdistribucionduasCargamasiva.html');
}

function validarDate(id) {
    console.log(id);
    value = $("#" + id).val();
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var fechaCompleta = value.match(datePat);
    if (fechaCompleta == null) {
        showError("Debe ingresar la fecha de programación.");
        document.getElementById(id).focus();
        return false;
    }

    dia = fechaCompleta[1];
    mes = fechaCompleta[3];
    anio = fechaCompleta[5];

    if (dia < 1 || dia > 31) {
        showError("El valor del día debe estar comprendido entre 1 y 31.");
        document.getElementById(id).focus();
        return false;
    }
    if (mes < 1 || mes > 12) {
        document.getElementById(id).focus();
        showError("El valor del mes debe estar comprendido entre 1 y 12.");
        return false;
    }
    if ((mes == 4 || mes == 6 || mes == 9 || mes == 11) && dia == 31) {
        showError("El mes " + mes + " no tiene 31 días!");
        return false;
    }
    if (mes == 2) { // bisiesto
        var bisiesto = (anio % 4 == 0 && (anio % 100 != 0 || anio % 400 == 0));
        if (dia > 29 || (dia == 29 && !bisiesto)) {
            document.getElementById(id).focus();
            showError("Febrero del " + anio + " no contiene " + dia + " dias!");
            return false;
        }
    }
    return true;
}

function ListarDUASFiltro() {

    var result = true;
    result = (
        (
            ($("#filtroFechaIni").val() == "" && $("#filtroFechaFin").val() == "") ||
            ($("#filtroFechaIni").val() != "" && $("#filtroFechaFin").val() == "") ||
            ($("#filtroFechaIni").val() == "" && $("#filtroFechaFin").val() != "")
        ) &&
        $("#filtroVolante").val() == "" &&
        $("#filtroDUA").val() == "" &&
        $("#filtroPaquete").val() == ""
        ) ? false : true;

    if (!result)
        showError("Debe ingresar correctamente los filtros de busqueda");

    if(result){
        $("#gridpadre").empty();
        var params = JSON.stringify({
            "key": "VTO/98UvYYI=",
            "parametros":
                [
                    $('#filtroFechaIni').val(),
                    $('#filtroFechaFin').val(),
                    $('#filtroVolante').val(),
                    $('#filtroDUA').val(),
                    $('#filtroPaquete').val(),
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
                                //dsgridpadre.group({
                                //    field: "v10", title: "Distribución",
                                //});
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
            height: 400,
            
            rowTemplate:
                        "<tr>" +
                        "<td><div title='Modificar' style='cursor:pointer'><center><i class=\"fa fa-pencil\" onclick=\"Modificar('#: v01 #'); return false;\">&nbsp;</i></center></div></td>" +
                        "<td><center>#: v02 #</center></td>" +
                        "<td><center>#: v03 #</center></td>" +
                        "<td><center>#: v04 #</center></td>" +
                        "<td><center>#: v05 #</center></td>" +
                        "<td><center>#: v06 #</center></td>" +
                        "<td><center>#: v07 #</center></td>" +
                        "<td><center>#: v08 #</center></td>" +
                        "<td><center><input type=\"image\" src=\"#: v09 #\" style=\"width:17px\" /></center></td>" +
                        "</tr>",
            altRowTemplate:
                        "<tr>" +
                        "<td><div title='Modificar' style='cursor:pointer'><center><i class=\"fa fa-pencil\" onclick=\"Modificar('#: v01 #'); return false;\">&nbsp;</i></center></div></td>" +
                        "<td><center>#: v02 #</center></td>" +
                        "<td><center>#: v03 #</center></td>" +
                        "<td><center>#: v04 #</center></td>" +
                        "<td><center>#: v05 #</center></td>" +
                        "<td><center>#: v06 #</center></td>" +
                        "<td><center>#: v07 #</center></td>" +
                        "<td><center>#: v08 #</center></td>" +
                        "<td><center><input type=\"image\" src=\"#: v09 #\" style=\"width:17px\" /></center></td>" +
                        "</tr>",
            group: {
                field: "v10"
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
                field: "v10", 
                title: "Distribución",
                hidden: true 
            },
            {
                template: "<div title='Modificar' style='cursor:pointer'><center><i class=\"fa fa-pencil\" onclick=\"Modificar('#: v01 #'); return false;\">&nbsp;</i></center></div>",
                title: "<div title='Nuevo' style='cursor:pointer'><center><i class='fa fa-plus' onclick='Nuevo(); return false;'>&nbsp;</i></center></div>",
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
                template: "<center><input type=\"image\" src=\"#: v09 #\" style=\"width:17px\" /></center>",
                title: "<center>Activo</center>",
                width: 60
            }
            ]
        });

        /*
        var grid = $('#gridpadre').data('kendoGrid');
        grid.dataSource.group({
            field: "v07", title: "Fecha de Programación",
        });
        grid.dataSource.read();
        */
    }

}