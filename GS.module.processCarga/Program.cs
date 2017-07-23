

namespace GS.module.processCarga
{
    using entities.Base;
    using entities.Primary;
    using logic.Common;
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    class Program
    {
        static void Main(string[] args)
        {
            LeerExcelDistribucionDUAs(args[0]);
            //LeerExcelDistribucionDUAs("dZGNmjmb7QDmoXC4p2gygn5N6JwfKq2GolTj/POxncvqy+9clT7f4A==");
        }

        public static string[] GetRange(string range, Microsoft.Office.Interop.Excel.Worksheet excelWorksheet)
        {
            Microsoft.Office.Interop.Excel.Range workingRangeCells =
              excelWorksheet.get_Range(range, Type.Missing);
            //workingRangeCells.Select();

            System.Array array = (System.Array)workingRangeCells.Cells.Value2;
            string[] arrayS = ConvertToStringArray(array);

            return arrayS;
        }

        static string[] ConvertToStringArray(System.Array values)
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
        public static void LeerExcelDistribucionDUAs(string key)
        {
            
            string folder = ConfigurationManager.AppSettings["distribucionDUAs"].ToString();
            try
            {
                foreach (var file in Directory.GetFiles(folder))
                {
                    string extension = file.Split('.')[file.Split('.').Length - 1];
                    if (extension.Equals("xls") || extension.Equals("xlsx"))
                    {

                        //MessageBox.Show("Si funciona");
                        foreach (Process clsProcess in Process.GetProcesses())
                            if (clsProcess.ProcessName.ToUpper().Equals("EXCEL"))
                                clsProcess.Kill();
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                        //MessageBox.Show("Lo de siempre");
                        Workbook wb = app.Workbooks.Open(
                            @file, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing);
                        //MessageBox.Show("Abrio el Libro");
                        Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[1];

                        Range excelRange = sheet.UsedRange;

                        List<BasicTen> ocol = new List<BasicTen>();
                        int index = 0;
                        string[] A4D4;

                        blCommon bl = new blCommon();
                        object[] para = { key };
                        object[] cryp = { };

                        Transaction transaction = bl.EjecutarTransaction("Kfz/n8tYV0U=", para.ToList(), cryp.ToList());
                        //MessageBox.Show("Eliminacion:"+ transaction.Message);
                        foreach (Microsoft.Office.Interop.Excel.Range row in excelRange.Rows)
                        {
                            List<object> parametros = new List<object>();
                            if (index != 0)
                            {
                                int rowNumber = row.Row;
                                A4D4 = GetRange("A" + rowNumber + ":H" + rowNumber + "", sheet);
                                
                                parametros.Add(A4D4[0]);
                                parametros.Add(A4D4[1]);
                                parametros.Add(A4D4[2]);
                                parametros.Add(A4D4[3]);
                                parametros.Add(A4D4[4]);

                                //"OK: registro con datos válidos"


                                int _valorSerie; int _valorBulto;
                                bool isNumberSerie = int.TryParse(A4D4[5], out _valorSerie);
                                bool isNumberBulto = int.TryParse(A4D4[6], out _valorBulto);

                                parametros.Add(!isNumberSerie ? 0 : int.Parse(A4D4[5]));
                                parametros.Add(!isNumberBulto ? 0 : int.Parse(A4D4[6]));
                                parametros.Add(A4D4[7]);

                                if (!isNumberSerie)
                                    parametros.Add("ERROR: El valor de la serie no tiene el formato correcto");
                                else if (!isNumberBulto)
                                    parametros.Add("ERROR: El valor del bulto no tiene el formato correcto");
                                else
                                    parametros.Add("OK: registro con datos válidos");

                                parametros.Add(key);
                                object[] cryp1 = { };
                                Transaction transaction1 = new blCommon().EjecutarTransaction("LpltZdxAvvk=", parametros, cryp1.ToList());
                                //MessageBox.Show("Inserccion:" + transaction.Message);
                            }
                            index++;

                            /*
                            int _valorSerie; int _valorBulto;
                            bool isNumberSerie = int.TryParse(A4D4[5], out _valorSerie);
                            bool isNumberBulto = int.TryParse(A4D4[6], out _valorBulto);

                            BasicTen temp = new BasicTen();
                            temp.v01 = rowNumber;
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
                            
                            */

                        }
                        



                        foreach (Process clsProcess in Process.GetProcesses())
                            if (clsProcess.ProcessName.ToUpper().Equals("EXCEL"))
                                clsProcess.Kill();
                        //Console.WriteLine("OK");
                        //Console.ReadLine();

                        Process program = Process.GetCurrentProcess();
                        program.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                var file = File.CreateText(folder + "log-002.txt");
                file.WriteLine(ex.Message.ToString());
                file.Close();
            }
        }
    }
}
