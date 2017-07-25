using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GS.modules.view.Content.ssii.planificacion
{
    public partial class viewdistribucionduasCargamasivaUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {

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
                    foreach (Process clsProcess in Process.GetProcesses())
                        if (clsProcess.ProcessName.ToUpper().Equals("EXCEL"))
                            clsProcess.Kill();

                    FileUploadPlantilla.PostedFile.SaveAs(path + FileUploadPlantilla.FileName);
                    //FileUploadPlantilla.PostedFile.SaveAs(path + @"dZGNmjmb7QDmoXC4p2gygn5N6JwfKq2GolTj/POxncvqy+9clT7f4A==" + ".xls");

                    foreach (Process clsProcess in Process.GetProcesses())
                        if (clsProcess.ProcessName.ToUpper().Equals("EXCEL"))
                            clsProcess.Kill();
                    try
                    {
                        label.Text = processCarga;
                        //System.Diagnostics.Process.Start(@"C:\Data\Publish\UpdaloadExcel\processCarga.exe");

                        System.Diagnostics.Process appy = new System.Diagnostics.Process();
                        appy.StartInfo.FileName = processCarga;
                        appy.StartInfo.Arguments = "dZGNmjmb7QDmoXC4p2gygn5N6JwfKq2GolTj/POxncvqy+9clT7f4A==";
                        appy.Start();
                        appy.WaitForExit();

                        Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess('La distribución se pre-cargó correctamente, favor espere unos minutos para pre-visualizar los resultados');", true);
                    }
                    catch (Exception ex)
                    {
                        var file = File.CreateText(path + "log-001.txt");
                        file.WriteLine(ex.Message.ToString());
                        file.Close();

                        Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess(\'" +
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
    }
}