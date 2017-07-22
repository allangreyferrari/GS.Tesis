using System;
using Microsoft.Office.Interop.Excel;
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

    public const string DataTempDua = "DataTempDua";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string[] GetRange(string range, Worksheet excelWorksheet)
    {
        Microsoft.Office.Interop.Excel.Range workingRangeCells =
          excelWorksheet.get_Range(range, Type.Missing);
        //workingRangeCells.Select();

        System.Array array = (System.Array)workingRangeCells.Cells.Value2;
        string[] arrayS = this.ConvertToStringArray(array);

        return arrayS;
    }

    string[] ConvertToStringArray(System.Array values)
    {

        // create a new string array
        string[] theArray = new string[values.Length];

        // loop through the 2-D System.Array and populate the 1-D String Array
        for (int i = 1; i <= values.Length; i++)
        {
            if (values.GetValue(1, i) == null)
                theArray[i - 1] = "";
            else
                theArray[i - 1] = (string)values.GetValue(1, i).ToString();
        }

        return theArray;
    }
    private static string host = ConfigurationManager.AppSettings["host"].ToString();

    public static string GetValue(string id, string key, string nodo)
    {
        string postData =
            "{" +
                "\"key\": \"" + id + "\"," +
                "\"parametros\": [\"" + key + "\"]," +
                "\"cryp\": []" +
            "}";

        WebRequest request = WebRequest.Create("http://" + host + "/wsCommon.svc/ListarBasicTwenty");
        request.Method = "POST";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        request.ContentType = "application/json";
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        reader.Close();
        dataStream.Close();
        response.Close();
        string value = new JavaScriptSerializer().Deserialize<string>(responseFromServer);
        return value;
    }

    protected void btnCargar_Click(object sender, EventArgs e) {
        Boolean fileOK = false;
        String path = "C:\\Data\\Publish\\upload\\";

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
                    if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                        clsProcess.Kill();
                FileUploadPlantilla.PostedFile.SaveAs(path + FileUploadPlantilla.FileName);
                try
                {

                    string sArchivo = path + FileUploadPlantilla.FileName;
                    
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(
                    sArchivo, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);

                    //Worksheet sheet = (Worksheet)wb.Sheets[1];
                    //Range excelRange = sheet.UsedRange;
                    Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess('" + "App" + "');", true);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess('"+ ex.Message+"');", true);
                }
                    //Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess('Save Archivo');", true);
                    /*
                    try
                    {

                        string sArchivo = path + FileUploadPlantilla.FileName;
                        
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                        Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(
                            sArchivo, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing);

                        Worksheet sheet = (Worksheet)wb.Sheets[1];
                        Range excelRange = sheet.UsedRange;

                        List<TempDua> ocol = new List<TempDua>();
                        int index = 0;
                        foreach (Microsoft.Office.Interop.Excel.Range row in excelRange.Rows)
                        {
                            int rowNumber = row.Row;
                            string[] A4D4 = GetRange("A" + rowNumber + ":H" + rowNumber + "", sheet);

                            int _valorSerie; int _valorBulto;
                            bool isNumberSerie = int.TryParse(A4D4[5], out _valorSerie);
                            bool isNumberBulto = int.TryParse(A4D4[6], out _valorBulto);

                            TempDua temp = new TempDua();
                            temp.v01 = index;
                            temp.v02 = A4D4[1] + "-" + A4D4[2];
                            temp.v03 = A4D4[3];
                            temp.v04 = A4D4[4];
                            temp.v05 = A4D4[7];
                            temp.v06 = A4D4[5];
                            temp.v07 = A4D4[6];
                            temp.v08 = "OK: registro con datos válidos";

                            if (!isNumberSerie)
                            {
                                temp.v06 = 0;
                                temp.v08 = "ERROR: El valor de la serie no tiene el formato correcto";
                            }
                            else
                                temp.v06 = _valorSerie;

                            if (!isNumberBulto)
                            {
                                temp.v07 = 0;
                                temp.v08 = "ERROR: El valor del bulto no tiene el formato correcto";
                            }
                            else
                                temp.v07 = _valorBulto;

                            if (index != 0)
                                ocol.Add(temp);
                            index++;
                        }
                        //System.Web.HttpContext.Current.Session[DataTempDua] = ocol;

                        //foreach (Process clsProcess in Process.GetProcesses())
                        //    if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                        //        clsProcess.Kill();
                        Page.ClientScript.RegisterStartupScript(btnCargar.GetType(), "showSuccessChild", "parent.showSuccess('Se detalla el estado de la información del archivo');", true);

                    }
                    catch (Exception childex)
                    {
                        foreach (Process clsProcess in Process.GetProcesses())
                            if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                                clsProcess.Kill();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorChild",
                        string.Format("parent.showError('{0}');", childex.Message), true);
                    }
                    */
                }
                catch (Exception ex)
            {
                foreach (Process clsProcess in Process.GetProcesses())
                    if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                        clsProcess.Kill();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorChild",
                    string.Format("parent.showError('{0}');", ex.Message), true);
            }
        }
        else
        {
            foreach (Process clsProcess in Process.GetProcesses())
                if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                    clsProcess.Kill();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showErrorChild",
                    string.Format("parent.showError('{0}');", "No ha sido posible procesar el archivo"), true);
        }

    }

    [WebMethod]
    public static List<TempDua> ListarBasicDua()
    {
        List<TempDua> list = (List<TempDua>)System.Web.HttpContext.Current.Session[DataTempDua];
       
        TempDua temp = new TempDua() { v02 = "hola", v03 =" si se podía", v04 = list==null?"0":list.Count.ToString() };
        List<TempDua> ocol = new List<TempDua>();
        ocol.Add(temp);
        return ocol;
        //return (List<TempDua>)System.Web.HttpContext.Current.Session[DataTempDua];
        //if (System.Web.HttpContext.Current.Session[DataTempDua] != null)
        //    return (List<TempDua>)System.Web.HttpContext.Current.Session[DataTempDua];
        //else
        //    return new List<TempDua>();

    }

    public class TempDua
    {
        public object v01 { get; set; }
        public object v02 { get; set; }
        public object v03 { get; set; }
        public object v04 { get; set; }
        public object v05 { get; set; }
        public object v06 { get; set; }
        public object v07 { get; set; }
        public object v08 { get; set; }
        public object v09 { get; set; }
        public object v10 { get; set; }
    }
}
