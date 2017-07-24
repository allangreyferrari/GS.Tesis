$(document).ready(function () {
    $("#filtroFechaProgramacion").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $('#filtroFechaProgramacion').datepicker({
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

    $("#filtroFechaProgramacion").change(function (event) {
        if ($("#filtroFechaProgramacion").val != "")
            setTimeout(ListarDUASCargaMasivaFecha, 1000);
    });

    $('#btnPlantilla').click(function (e) {
        e.preventDefault(); 
        window.location.href = rootInit + '/module/Content/ssii/planificacion/PlantillaCargaDUA.xls';
    });
});


function CargarArchivo()
{
    var result = validarDate("filtroFechaProgramacion");
    if (result) {
        if (DataEXCEL == null) {
            showError("Debe realizar la previsualizacion de la data a cargar.");
            document.getElementById('xlf').focus();
            result = false;
            return;
        }
    }
    if (result) {
        $("#popupFechaProgramacion").text($('#filtroFechaProgramacion').val());
        $("#modalconfirmar").modal("show");
    }    
}

function ConfirmarPopup()
{
    console.log(DataEXCEL[0]);
    var params = JSON.stringify({
        "parametros":
            [
                $("#filtroFechaProgramacion").val(),
                JSON.stringify(DataEXCEL),
                localStorage.getItem('session')
            ]
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCommon.svc/EjecutarCargaEXCEL",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: true,
        processData: false,
        cache: false,
        success: function (transaction) {
            if (transaction.Type == 0){
                showSuccess(transaction.Message);
                $("#modalconfirmar").modal("hide");
                setTimeout(IrCargarDistribucionDuas, 2000);   
            }
            else
                showError(transaction.Message);
            
            
        },
        error: function (transaction) {
            showError(JSON.stringify(transaction.Message));
        }
    });
}

function CancelarPopup()
{
    $("#modalconfirmar").modal("hide");
    DataEXCEL = null;
}

function validarDate(id) {
    console.log(id);
    value = $("#"+id).val();
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


function IrCargarDistribucionDuas()
{
    argument = null;
    $("#pages").load('../../Content/ssii/planificacion/viewdistribucionduas.html');
}


function ListarDUASCargaMasivaTemp() {
    $("#gridpadre").empty();

    var dsgridpadre = new kendo.data.DataSource({
        data: DataEXCEL
    });

    $("#gridpadre").kendoGrid({
        dataSource: dsgridpadre,
        height: 400,
        rowTemplate:
                    "<tr>" +
                    "<td>#: Año #</td>" +
                    "<td>#: Dua #</td>" +
                    "<td>#: Guia #</td>" +
                    "<td>#: Volante #</td>" +
                    "<td>#: Serie #</td>" +
                    "<td>#: Bultos #</td>" +
                    "<td>#: Paquete #</td>" +
                    "<td style=\"color:#: color #;font-weight: bold\" >#: Observacion #</td>" +
                    "</tr>",
        altRowTemplate:
                    "<tr>" +
                    "<td>#: Año #</td>" +
                    "<td>#: Dua #</td>" +
                    "<td>#: Guia #</td>" +
                    "<td>#: Volante #</td>" +
                    "<td>#: Serie #</td>" +
                    "<td>#: Bultos #</td>" +
                    "<td>#: Paquete #</td>" +
                    "<td style=\"color:#: color #;font-weight: bold\" >#: Observacion #</td>"+
                    "</tr>",
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
        //{
        //    field: "Fecha",
        //    title: "<center>Fecha</center>",
        //    width: 100
        //},
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
        },
        {
            field: "Observacion",
            title: "<center>Observación</center>",
            width: 300
        }
        ]
    });
}

function ListarDUASCargaMasivaFecha() {

    $("#gridpadre").empty();
    var params = JSON.stringify({
        "key": "Ph8qkXse7vY=",
        "parametros":
            [
                $('#filtroFechaProgramacion').val(),
                localStorage.getItem('session')
            ],
        "cryp": []
    });

    var dsgridpadre = new kendo.data.TreeListDataSource({
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
                        if (response.NroRows > 0)
                        {
                            showWarning("La fecha " + $('#filtroFechaProgramacion').val() + " tiene DUAs programadas");
                        }
                        options.success(response.Rows);
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
                    "<td>#: v01 #</td>" +
                    "<td>#: v02 #</td>" +
                    "<td>#: v03 #</td>" +
                    "<td>#: v04 #</td>" +
                    "<td>#: v05 #</td>" +
                    "<td>#: v06 #</td>" +
                    "<td>#: v07 #</td>" +
                    "<td style=\"color:#: v09 #;font-weight: bold\" >#: v08 #</td>" +
                    "</tr>",
        altRowTemplate:
                    "<tr>" +
                    "<td>#: v01 #</td>" +
                    "<td>#: v02 #</td>" +
                    "<td>#: v03 #</td>" +
                    "<td>#: v04 #</td>" +
                    "<td>#: v05 #</td>" +
                    "<td>#: v06 #</td>" +
                    "<td>#: v07 #</td>" +
                    "<td style=\"color:#: v09 #;font-weight: bold\" >#: v08 #</td>" +
                    "</tr>",
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
        //{
        //    field: "Fecha",
        //    title: "<center>Fecha</center>",
        //    width: 100
        //},
        {
            field: "v01",
            title: "<center>Año</center>",
            width: 100
        },
        {
            field: "v02",
            title: "<center>Número DUA</center>",
            width: 100
        },
        {
            field: "v03",
            title: "<center>Guía</center>",
            width: 120
        },
        {
            field: "v04",
            title: "<center>Volante</center>",
            width: 100
        },
        {
            field: "v05",
            title: "<center>Serie</center>",
            width: 60
        },
        {
            field: "v06",
            title: "<center>Bultos</center>",
            width: 60
        },
        {
            field: "v07",
            title: "<center>Paquete</center>",
            width: 100
        },
        {
            field: "v08",
            title: "<center>Observación</center>",
            width: 300
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