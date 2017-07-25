<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewdistribucionduasCargamasivaUp.aspx.cs" Inherits="GS.modules.view.Content.ssii.planificacion.viewdistribucionduasCargamasivaUp" %>
<form id="form1" runat="server">
<link href="/moduleresources/css/home.css" rel="stylesheet" type="text/css" />
<link href="/moduleresources/css/font-gs.min.css" rel="stylesheet" type="text/css" />
<style> 
    table, th, td { border: 0px ; padding: 0px; } 
    table { border-spacing: 0px; } 
</style>
<script type="text/javascript" language="javascript">
    function checkfile(sender) {
        var validExts = new Array(".xls", ".xlsx");
        var fileExt = sender.value;
        fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
        if (validExts.indexOf(fileExt) < 0) {
            parent.showError("No ha sido posible procesar el archivo");
            sender.value = "";
            return false;
        }
        else {
            document.getElementById('<%=btnCargar.ClientID%>').click();
            return true;
        }
    }
</script>
<div style="margin:0px;">    
    <table style="width:100%" cellspacing ="0px" cellpanding ="0px">
        <tr>
            <td>
                <asp:FileUpload ID="FileUploadPlantilla" onchange="checkfile(this);" runat="server" CssClass="upload-buttom" accept=".xls,.xlsx" />   
                <asp:Label runat="server" id="label"></asp:Label>
            </td>
<%--            <td>
                <div class="home-buttom" style="width:90px; height:20px;" 
                    onclick="document.getElementById('<%=btnCargar.ClientID%>').click();">
                    <center><i class="fa fa-cloud-upload">&nbsp;&nbsp;</i> Cargar</center>
                </div>
            </td>--%>
        </tr>
    </table>
    <div style="display:none" >
        <asp:Button runat="server" text="" id="btnCargar" OnClick="btnCargar_Click" />  
    </div>
    
</div>
</form>


