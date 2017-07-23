using System;
using System.Collections.Generic;
using System.Web.UI;

using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Web.Services;

public partial class Content_ssii_planificacion_viewdistribucionduasCargamasivaUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCargar_Click(object sender, EventArgs e) {

        Boolean fileOK = false;
        String path = ConfigurationManager.AppSettings["folderUploadDuas"].ToString();
        String processCarga = ConfigurationManager.AppSettings["processCarga"].ToString();

        if (FileUploadPlantilla.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(FileUploadPlantilla.FileName).ToLower();
            String[] allowedExtensions = { ".xls", ".xlsx" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
        }
        if (fileOK)
        {
            try
            {
                FileUploadPlantilla.PostedFile.SaveAs(path + FileUploadPlantilla.FileName);
                try
                {
                    label.Text = processCarga;
                    //System.Diagnostics.Process.Start(@"C:\Data\Publish\UpdaloadExcel\processCarga.exe");

                    System.Diagnostics.Process appy = new System.Diagnostics.Process();
                    appy.StartInfo.FileName = @"C:\Data\Dev\PI\GS.module.processCarga\bin\Debug\GS.module.processCarga.exe";
                    appy.StartInfo.Arguments = "5YUF4Mk8EytRjE0l7vI8Zb02q3Nj/abGolTj/POxncvqy+9clT7f4A==";
                    appy.Start();
                    appy.WaitForExit();

                    Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess('La distribución se pre-cargó correctamente, favor espere unos minutos para pre-visualizar los resultados');", true);
                }
                catch (Exception ex)
                {
                    var file = File.CreateText(path + "log-001.txt");
                    file.WriteLine(ex.Message.ToString());
                    file.Close();

                    Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess(\'"+ 
                    ex.Message.ToString().Replace(Char.ConvertFromUtf32(34), "").Replace(Char.ConvertFromUtf32(10), "\\n").Replace(Char.ConvertFromUtf32(13), "\\n").Replace(")", "-").Replace("(", "-").Replace("\\", "|") +
                    "\');", true);
                }

            }
            catch (Exception ex)
            {
                var file = File.CreateText(path + "log-001.txt");
                file.WriteLine(ex.Message.ToString());
                file.Close();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorChild",
                    string.Format("parent.showError('{0}');", ex.Message), true);
            }
        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorChild",
                    string.Format("parent.showError('{0}');", "No ha sido posible procesar el archivo"), true);
        }

    }



    //[WebMethod]
    //public static List<TempDua> ListarBasicDua()
    //{
    //    List<TempDua> list = (List<TempDua>)System.Web.HttpContext.Current.Session[DataTempDua];
       
    //    TempDua temp = new TempDua() { v02 = "hola", v03 =" si se podía", v04 = list==null?"0":list.Count.ToString() };
    //    List<TempDua> ocol = new List<TempDua>();
    //    ocol.Add(temp);
    //    return ocol;
    //    //return (List<TempDua>)System.Web.HttpContext.Current.Session[DataTempDua];
    //    //if (System.Web.HttpContext.Current.Session[DataTempDua] != null)
    //    //    return (List<TempDua>)System.Web.HttpContext.Current.Session[DataTempDua];
    //    //else
    //    //    return new List<TempDua>();

    //}

    //public class TempDua
    //{
    //    public object v01 { get; set; }
    //    public object v02 { get; set; }
    //    public object v03 { get; set; }
    //    public object v04 { get; set; }
    //    public object v05 { get; set; }
    //    public object v06 { get; set; }
    //    public object v07 { get; set; }
    //    public object v08 { get; set; }
    //    public object v09 { get; set; }
    //    public object v10 { get; set; }
    //}
}
