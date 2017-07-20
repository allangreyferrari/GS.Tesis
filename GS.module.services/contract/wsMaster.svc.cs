using GS.module.entities.Base;
using GS.module.entities.Seguridad;
using GS.module.logic.IT;
using GS.module.logic.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace GS.module.services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "wsConfiguracion" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione wsConfiguracion.svc o wsConfiguracion.svc.cs en el Explorador de soluciones e inicie la depuración.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class wsMaster : IwsMaster
    {
        public MasterCollection ListarOperacionesTesoreria(string bd, string orden, string fechaini, string fechafin, int check1, int check2)
        {
            blMaster bl = new blMaster();
            Transaction transaction = Helper.InitTransaction();
            List<object> parametros = new List<object>();
            parametros.Add(orden == string.Empty ? -1 : Convert.ToInt32(orden));
            parametros.Add(fechaini == string.Empty ? null: fechaini);
            parametros.Add(fechafin == string.Empty ? null: fechafin);
            parametros.Add(check1);
            parametros.Add(check2);
            List<Master> ocol = new List<Master>();
            if (bd == "all")
            {
                /*
                ocol.AddRange(bl.ListarMaster("REC_Sil..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                */
                ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows); 
                
                return new MasterCollection(ocol, transaction);
            }
            else if (bd == "sil")
                /*return bl.ListarMaster("REC_Sil..VBG00385_GESTIONCHECK", parametros, out transaction);*/
                return bl.ListarMaster("Silvestre_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction);
            else if (bd == "neo")
                return bl.ListarMaster("NeoAgrum_SAC..VBG00385_GESTIONCHECK", parametros, out transaction);
            else if (bd == "ina")
                return bl.ListarMaster("Inatec_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction);
            else
                return new MasterCollection(new List<Master>(), transaction);
        }

        public MasterCollection ListarOperacionesTesoreriaEV(string bd, string orden, string fechaini, string fechafin, int check1, int check2)
        {
            blMaster bl = new blMaster();
            Transaction transaction = Helper.InitTransaction();
            List<object> parametros = new List<object>();
            parametros.Add(orden == string.Empty ? -1 : Convert.ToInt32(orden));
            parametros.Add(fechaini == string.Empty ? null : fechaini);
            parametros.Add(fechafin == string.Empty ? null : fechafin);
            parametros.Add(check1);
            parametros.Add(check2);
            List<Master> ocol = new List<Master>();
            if (bd == "all")
            {
                /*
                ocol.AddRange(bl.ListarMaster("REC_Sil..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                */
                ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);

                return new MasterCollection(ocol, transaction);
            }
            else if (bd == "sil")
                /*return bl.ListarMaster("REC_Sil..VBG00385_GESTIONCHECK", parametros, out transaction);*/
                return bl.ListarMaster("Silvestre_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction);
            else if (bd == "neo")
                return bl.ListarMaster("NeoAgrum_SAC..VBG00414_GESTIONCHECK", parametros, out transaction);
            else if (bd == "ina")
                return bl.ListarMaster("Inatec_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction);
            else
                return new MasterCollection(new List<Master>(), transaction);
        }

        public MasterCollection ListarOperacionesTesoreriaALL(string bd, string documento, string orden, string fechaini, string fechafin, int check1, int check2)
        {
            blMaster bl = new blMaster();
            Transaction transaction = Helper.InitTransaction();
            List<object> parametros = new List<object>();
            parametros.Add(orden == string.Empty ? -1 : Convert.ToInt32(orden));
            parametros.Add(fechaini == string.Empty ? null : fechaini);
            parametros.Add(fechafin == string.Empty ? null : fechafin);
            parametros.Add(check1);
            parametros.Add(check2);
            List<Master> ocol = new List<Master>();

            switch (bd)
            {
                case "all":
                    switch (documento)
                    {
                        case "all":
                            ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            return new MasterCollection(ocol, transaction);
                        case "solegr":
                            ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            return new MasterCollection(ocol, transaction);
                        case "egrvar":
                            ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            return new MasterCollection(ocol, transaction);
                        default:
                            return new MasterCollection(new List<Master>(), transaction);
                    }
                case "sil":
                    switch (documento)
                    {
                        case "all":
                            ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Silvestre_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            return new MasterCollection(ocol, transaction);
                        case "solegr":
                            return bl.ListarMaster("Silvestre_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction);
                        case "egrvar":
                            return bl.ListarMaster("Silvestre_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction);
                        default:
                            return new MasterCollection(new List<Master>(), transaction);
                    }
                case "neo":
                    switch (documento)
                    {
                        case "all":
                            ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("NeoAgrum_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            return new MasterCollection(ocol, transaction);
                        case "solegr":
                            return bl.ListarMaster("NeoAgrum_SAC..VBG00385_GESTIONCHECK", parametros, out transaction);
                        case "egrvar":
                            return bl.ListarMaster("NeoAgrum_SAC..VBG00414_GESTIONCHECK", parametros, out transaction);
                        default:
                            return new MasterCollection(new List<Master>(), transaction);
                    }
                case "ina":
                    switch (documento)
                    {
                        case "all":
                            ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction).Rows);
                            ocol.AddRange(bl.ListarMaster("Inatec_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction).Rows);
                            return new MasterCollection(ocol, transaction);
                        case "solegr":
                            return bl.ListarMaster("Inatec_Peru_SAC..VBG00385_GESTIONCHECK", parametros, out transaction);
                        case "egrvar":
                            return bl.ListarMaster("Inatec_Peru_SAC..VBG00414_GESTIONCHECK", parametros, out transaction);
                        default:
                            return new MasterCollection(new List<Master>(), transaction);
                    }
                default:
                    return new MasterCollection(new List<Master>(), transaction);
            }
                
        }

        public string AprobarOperacionesTesoreria(string Op, string Bd, string Clave)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            blMaster bl = new blMaster();
            Transaction transaction = Helper.InitTransaction();
            List<object> parametros = new List<object>();            
            parametros.Add("CtasCtes");
            parametros.Add(Convert.ToInt32(Op));            
            parametros.Add(user.Alias);
            parametros.Add(Clave);
            if(Bd=="sil")
                /*
                bl.EjecutarTransaccion("REC_Sil..VBG01076_VALUSUA", parametros, out transaction);
                */            
                bl.EjecutarTransaccion("Silvestre_Peru_SAC..VBG01076_VALUSUA", parametros, out transaction);
            else if(Bd=="neo")
                bl.EjecutarTransaccion("NeoAgrum_SAC..VBG01076_VALUSUA", parametros, out transaction);
            else if (Bd == "ina")
                bl.EjecutarTransaccion("Inatec_Peru_SAC..VBG01076_VALUSUA", parametros, out transaction);
            
            if (transaction.type == TypeTransaction.OK)
                return Helper.InvokeTextHTML
                (
                    "$('#inPopupOp').html(\"\");"+
                    "$('#inPopupArea').val(\"\");" +
                    "$('#inPopupMonto').val(\"\");" +
                    "$('#inPopupConcepto').val(\"\");" +
                    "$('#inPopupOp').val(\"\");" +
                    "$('#inPopupBd').val(\"\");" +
                    "ListarOperaciones();" +
                    "$('#confirmaraprobarModal').modal(\"hide\");" +
                    "showSuccess('Se realizó la 2da aprobación de la Op');"
                );
            else
                return Helper.InvokeErrorHTML(transaction.message);
                //return Common.InvokeErrorHTML(Op + " " + Bd + " " + Clave);
        }

        public MasterCollection ListarEmpresas()
        {
            List<Master> ocol = new List<Master>();
            ocol.Add(new Master { v01 = "all", v02 = "[ TODOS ]" });
            ocol.Add(new Master { v01 = "sil", v02 = " SILVESTRE PERÚ SAC " });
            ocol.Add(new Master { v01 = "neo", v02 = " NEOAGRUM SAC " });
            ocol.Add(new Master { v01 = "ina", v02 = " INATEC PERÚ SAC " });
            return new MasterCollection(ocol, Helper.GetTransaction(TypeTransaction.OK, ""));
        }

        public MasterCollection ListarEstados()
        {
            List<Master> ocol = new List<Master>();
            ocol.Add(new Master { v01 = "-1", v02 = "[ TODOS ]" });
            ocol.Add(new Master { v01 = "0", v02 = " NO CONFIRMADO " });
            ocol.Add(new Master { v01 = "1", v02 = " CONFIRMADO " });
            return new MasterCollection(ocol, Helper.GetTransaction(TypeTransaction.OK, ""));
        }

        public MasterCollection ListarDocumentos()
        {
            List<Master> ocol = new List<Master>();
            ocol.Add(new Master { v01 = "all", v02 = "[ TODOS ]" });
            ocol.Add(new Master { v01 = "solegr", v02 = " SOLICITUD DE EGRESO " });
            ocol.Add(new Master { v01 = "egrvar", v02 = " EGRESO VARIOS " });
            return new MasterCollection(ocol, Helper.GetTransaction(TypeTransaction.OK, ""));
        }

        private struct dia
        {
            public string nombre { get; set; }
            public double ini { get; set; }
            public double fin { get; set; }
        }
        public string[] ObtieneRangoBusquedaAprobacion()
        {
            try { 
                List<dia> dias = new List<dia>();
                dias.Add(new dia() { nombre = "lunes", ini = 3, fin = 3 });
                dias.Add(new dia() { nombre = "martes", ini = 4, fin = 2 });
                dias.Add(new dia() { nombre = "miércoles", ini = 5, fin = 1 });
                dias.Add(new dia() { nombre = "jueves", ini = 6, fin = 0 });
                dias.Add(new dia() { nombre = "viernes", ini = 0, fin = 6 });
                dias.Add(new dia() { nombre = "sábado", ini = 1, fin = 5 });
                dias.Add(new dia() { nombre = "domingo", ini = 2, fin = 4 });
                string nombre = DateTime.Now.ToString("dddd");
                string fechainicial = DateTime.Now.AddDays(dias.Where(be => be.nombre == nombre).ToList()[0].ini * -1.00).ToShortDateString();
                string fechafinal = DateTime.Now.AddDays(dias.Where(be => be.nombre == nombre).ToList()[0].fin).ToShortDateString();
                return new string[]{ fechainicial, fechafinal,"ok"};
            }
            catch(Exception ex)
            {
                return new string[] { "", "", ex.Message };
            }
        }
    }
}
