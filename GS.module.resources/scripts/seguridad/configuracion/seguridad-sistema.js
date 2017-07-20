$(document).ready(function () {
    $("#filtroEstado").attr("src","../../../moduleresources/images/check.png");
    $("#filtroboolEstado").val("true");
    ListarSistemas();
});

function CambiarEstado()
{    
    if($("#PopupSistemaboolEstado").val() == "true")
    {
        $("#PopupSistemaEstado").attr("src", "../../../moduleresources/images/uncheck.png");
        $("#PopupSistemaboolEstado").val("false");
    }
    else
    {
        $("#PopupSistemaEstado").attr("src", "../../../moduleresources/images/check.png");
        $("#PopupSistemaboolEstado").val("true");
    }
}

function CambiarFiltroEstado()
{    
    if($("#filtroboolEstado").val() == "true")
    {
        $("#filtroEstado").attr("src", "../../../moduleresources/images/uncheck.png");
        $("#filtroboolEstado").val("false");
    }
    else
    {
        $("#filtroEstado").attr("src", "../../../moduleresources/images/check.png");
        $("#filtroboolEstado").val("true");
    }
}

function EditarSistema(id)
{
    var params = JSON.stringify({
        "key": "PIV8o1/cmq0=",
        "parametros":
            [
                id,
                "",
                "",
                0,
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
            if (response.NroRows = 0)
                showError("Ocurrio un error al interntar recuperar el registro");
       
            $("#PopupSistemaHeader").html("<i class=\"fa fa-pencil\">&nbsp;</i> Editar Sistema");    
            $("#PopupSistemaId").val(response.Rows[0].v01);
            $("#PopupSistemaNombre").val(response.Rows[0].v02);
            $("#PopupSistemaDescripcion").val(response.Rows[0].v03);
            $("#PopupSistemaKey").val(response.Rows[0].v05);
            $("#PopupSistemaPoolName").val(response.Rows[0].v07);
            $("#PopupSistemaFileconfig").val(response.Rows[0].v08);
            //$("#PopupSistemaEncriptado").val("id: " + response.Rows[0].v10 + "\n" + "pass: " + response.Rows[0].v04);
            $("#PopupSistemaEstado").attr("src", response.Rows[0].v09);
            $("#PopupSistemaboolEstado").val(response.Rows[0].v06);
            //$("#PopupSistemaEncriptado").prop("disabled", false);
            $("#PopupSistemaPassword").prop("disabled", true);
            $("#PopupSistema").modal("show");  
        },
        error: function (response) {
        }
    });
}

function ReciclarPool()
{
    var params = JSON.stringify({
	    "id" : $("#PopupSistemaId").val(),
	    "namepool" : $("#PopupSistemaPoolName").val()
	});
    $.ajax({
        type: "POST",
        url: wsnode + "wsConfiguracion.svc/ReciclarPool",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (transaction) { 
            if(transaction.type==0)
                showSuccess(transaction.message);
            else
                showError(transaction.message);
            $("#PopupSistema").modal("hide");  
            ListarSistemas();
        },
        error: function (transaction) {
        }
    });
}
function GuardarSistema()
{
    var params = JSON.stringify({
	"oe":{
		 "id" : $("#PopupSistemaId").val(),
		 "nombre" : $("#PopupSistemaNombre").val(),
		 "descripcion" : $("#PopupSistemaDescripcion").val(),
		 "key" : $("#PopupSistemaKey").val(),
		 "estado" : $("#PopupSistemaboolEstado").val(),
         "password": $("#PopupSistemaPassword").val(),
         "fileconfig": $("#PopupSistemaFileconfig").val(),
         "poolname": $("#PopupSistemaPoolName").val()
		}
	});
    //console.log(params);    
    var method = $("#PopupSistemaId").val()=="0" ? "InsertarSistema" : "EditarSistema";
    $.ajax({
        type: "POST",
        url: wsnode + "wsConfiguracion.svc/" + method,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (transaction) { 
            if(transaction.type==0)
                showSuccess(transaction.message);
            else
                showError(transaction.message);
            $("#PopupSistema").modal("hide");  
            ListarSistemas();
        },
        error: function (transaction) {
        }
    });
}

function RegistrarSistema()
{
    $("#PopupSistemaHeader").html("<i class=\"fa fa-plus\">&nbsp;</i> Registrar Sistema");    
    $("#PopupSistemaId").val("0");
    $("#PopupSistemaNombre").val("");
    $("#PopupSistemaDescripcion").val("");
    $("#PopupSistemaKey").val("");
    $("#PopupSistemaPassword").val("");
    $("#PopupSistemaPoolName").val("");
    $("#PopupSistemaFileconfig").val("");
    $("#PopupSistemaEstado").attr("src","../resources/images/uncheck.png");
    $("#PopupSistemaboolEstado").val("false"); 
    $("#PopupSistema").modal("show");     
}

function MostrarOpciones(id)
{
    argument = id;
    $("#pages").load('configuracion/viewSistemaOpcion.html');
}

function ListarSistemas() {
    var params = JSON.stringify({
        "key": "PIV8o1/cmq0=",
        "parametros": 
            [
                0, 
                $("#filtroNombre").val(),
                $("#filtroDescripcion").val(),
                0, //$("#filtroboolEstado").val() == "" ? 0 : parseInt($("#filtroboolEstado").val()),
                localStorage.getItem('session')
            ],
        "cryp": []
    });
    var dsgridpadre = new kendo.data.DataSource({
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
                            dsgridpadre.pageSize(20);
                        options.success(response.Rows);                                                
                    },
                    error: function (err) { }
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
            title: "Código",
            hidden: true
        },
        {
            template:
                "<div title='Editar Sistema' style='cursor:pointer'><center><i class=\"fa fa-pencil\" onclick=\"EditarSistema('#: v01 #'); return false;\">&nbsp;</i></center></div>",
            field: "v01",
            title:
                "<div title='Registrar Sistema' style='cursor:pointer'><center><i class='fa fa-plus' onclick='RegistrarSistema(); return false;'>&nbsp;</i></center></div>",
            width: 50
        },
        {
            template:
                "<div title='Opciones del Sistema' style='cursor:pointer'><center><i class=\"fa fa-list-ol\" onclick=\"MostrarOpciones('#: v01 #'); return false;\">&nbsp;</i></center></div>",
            width: 50
        },
        {
            field: "v02",
            title: "Nombre",
            width: 180
        },
        {
            field: "v03",
            title: "Descripcion",
            width: 300
        },
        {
            field: "v05",
            title: "Key",
            width: 120
        }, 
        {
            template: "<center><input type=\"image\" src=\"#: v09 #\" style=\"width:17px\" /></center>",
            field: "v09",
            title: "Activo",
            width: 70
        }
		]
    });
}
