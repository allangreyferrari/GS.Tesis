namespace GS.module.services
{
    using entities.Base;
    using entities.Primary;
    using logic.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "wsCommon" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione wsCommon.svc o wsCommon.svc.cs en el Explorador de soluciones e inicie la depuración.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class wsCommon : IwsCommon
    {
        #region String
        public StringTwoCollection ListarStringTwo(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringTwo(key, parametros.ToList(), cryp.ToList());
        }
        public StringFiveCollection ListarStringFive(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringFive(key, parametros.ToList(), cryp.ToList());
        }
        public StringTenCollection ListarStringTen(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringTen(key, parametros.ToList(), cryp.ToList());
        }
        public StringTwentyCollection ListarStringTwenty(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringTwenty(key, parametros.ToList(), cryp.ToList());
        }
        public StringThirtyCollection ListarStringThirty(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringThirty(key, parametros.ToList(), cryp.ToList());
        }
        public StringFiftyCollection ListarStringFifty(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringFifty(key, parametros.ToList(), cryp.ToList());
        }
        public StringSeventyCollection ListarStringSeventy(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringSeventy(key, parametros.ToList(), cryp.ToList());
        }
        public StringHundredCollection ListarStringHundred(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarStringHundred(key, parametros.ToList(), cryp.ToList());
        }
        #endregion

        #region Basic
        public BasicTwoCollection ListarBasicTwo(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicTwo(key, parametros.ToList(), cryp.ToList());
        }
        public BasicFiveCollection ListarBasicFive(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicFive(key, parametros.ToList(), cryp.ToList());
        }
        public BasicTenCollection ListarBasicTen(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicTen(key, parametros.ToList(), cryp.ToList());
        }
        public BasicTwentyCollection ListarBasicTwenty(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicTwenty(key, parametros.ToList(), cryp.ToList());
        }
        public BasicThirtyCollection ListarBasicThirty(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicThirty(key, parametros.ToList(), cryp.ToList());
        }
        public BasicFiftyCollection ListarBasicFifty(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicFifty(key, parametros.ToList(), cryp.ToList());
        }
        public BasicSeventyCollection ListarBasicSeventy(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicSeventy(key, parametros.ToList(), cryp.ToList());
        }
        public BasicHundredCollection ListarBasicHundred(string key, object[] parametros, object[] cryp = null)
        {
            return new blCommon().ListarBasicHundred(key, parametros.ToList(), cryp.ToList());
        }
        #endregion

        #region Session
        public string ObtenerSessionID()
        {
            return HttpContext.Current.Session.SessionID;
        }
        #endregion

        #region Transaction
        public Transaction EjecutarTransaction(string key, object[] parametros, object[] cryp = null)
        { 
            return new blCommon().EjecutarTransaction(key, parametros.ToList(), cryp.ToList());
        }

        private struct DUA
        {
            public string Año { get; set; }
            public string Dua { get; set; }
            public string Guia { get; set; }
            public string Volante { get; set; }
            public string Serie { get; set; }
            public string Bultos { get; set; }
            public string Paquete { get; set; }
            public string Observacion { get; set; }
            public string color { get; set; }

        }

        public Transaction EjecutarCargaEXCEL(object[] parametros)
        {
            try
            {
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                List<DUA> Excel = new List<DUA>();
                Excel = oJS.Deserialize<List<DUA>>(parametros[1].ToString());

                blCommon bl = new blCommon();
                object[] arguments = { parametros[0].ToString(), parametros[2].ToString() };
                object[] cryp = { };
                Transaction transaction = bl.EjecutarTransaction("0JTHp/wwjYI=", arguments.ToList(), cryp.ToList());
                foreach (var rowExcel in Excel.ToList())
                {
                    if (rowExcel.color == "green") { 
                        object[] argumentsChild = 
                        {
                            rowExcel.Año + "-" + rowExcel.Dua,
                            Convert.ToInt32(rowExcel.Serie),
                            parametros[0].ToString(),
                            rowExcel.Paquete,
                            rowExcel.Guia,
                            rowExcel.Volante,
                            Convert.ToInt32(rowExcel.Bultos),
                            parametros[2].ToString()
                        };
                        transaction = bl.EjecutarTransaction("KmKU4sRGvxY=", argumentsChild.ToList(), cryp.ToList());
                    }
                }
                transaction.Type = TypeTransaction.OK;
                transaction.Message = "Se procesaron " + Excel.Where(be=> be.color == "green").Count().ToString() + " DUAs para la fecha " + parametros[0].ToString();
                return transaction;
            }
            catch(System.Exception ex)
            {
                return new Transaction() { Type = TypeTransaction.OK, Message = ex.Message };
            }

            //
        }
        #endregion
        ~wsCommon() { }

        #region Opciones - Temporal
        string GetOptionsChildren(List<Option> options, int idPadre)
        {
            bool finish = false;
            StringBuilder builder = new StringBuilder();
            foreach (Option option in options.Where(be => be.CodigoPadre == idPadre))
            {
                switch (option.TipoApertura)
                {
                    case 4:
                        builder.Append(string.Format("<li class=\"dropdown-header\"><i class=\"{0}\"></i>&nbsp;&nbsp;{1}</li>", option.Abreviatura, option.Nombre.ToUpper()));
                        var html = GetOptionsChildren(options, option.Codigo);
                        if (html != string.Empty)
                            builder.Append(html);
                        builder.Append(string.Format("<li class=\"divider\"></li>"));
                        break;
                    case 1:
                        builder.Append(string.Format("<li><a href= \"#\" onclick=\"OpenPage('{1}');return false;\" ><i class=\"{0}\"></i>&nbsp;&nbsp;{2}</a></li>", option.Abreviatura, option.Ruta, option.Nombre));
                        //builder.Append(string.Format("<li><a href= \"#\" onclick=\"OpenPage('{0}');return false;\"> {1}</a></li>", option.RT_OPCION, option.NO_OPCION));
                        break;
                    default:
                        finish = true;
                        break;
                }
            }
            if (finish)
                builder.Append(@"</ul></li>");
            return builder.ToString();
        }

        public string GetOptions()
        {
            List<Option> options = new List<Option>();
            options.Add(new Option() { Codigo = 1, Nombre = "Módulo de Inspecciones Física", Ruta = "", CodigoPadre = 0, Nivel = "1", TipoApertura = 3, TipoRuta = "1", Abreviatura = "fa fa-fw fa-cubes" });
            options.Add(new Option() { Codigo = 2, Nombre = "Planificación", Ruta = "", CodigoPadre = 1, Nivel = "2", TipoApertura = 4, TipoRuta = "1", Abreviatura = "fa fa-fw fa-cog" });
            options.Add(new Option() { Codigo = 3, Nombre = "Cargar Distribución de dúas para la atención", Ruta = "../../Content/ssii/planificacion/viewdistribucionduas.html", CodigoPadre = 2, Nivel = "1", TipoApertura = 1, TipoRuta = "1", Abreviatura = "fa fa-fw fa-cloud-upload" });
            options.Add(new Option() { Codigo = 4, Nombre = "Ejecución", Ruta = "", CodigoPadre = 1, Nivel = "2", TipoApertura = 4, TipoRuta = "1", Abreviatura = "fa fa-fw fa-cog" });
            options.Add(new Option() { Codigo = 5, Nombre = "Actualizar Informacion de Ejecucion de Inspecciones Fisicas", Ruta = "../../Content/ssii/ejecucion/viewinspeccionesfisicas.html", CodigoPadre = 4, Nivel = "1", TipoApertura = 1, TipoRuta = "1", Abreviatura = "fa fa-fw fa-cubes" });

            if (options.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (Option option in options.Where(be => be.CodigoPadre == 0))
                {
                    switch (option.TipoApertura)
                    {
                        case 3:
                            builder.Append("<li class=\"dropdown\">");
                            builder.Append(string.Format("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\"><i class=\"{0}\"></i>&nbsp;&nbsp;{1} <span class=\"caret\"></span></a>", option.Abreviatura, option.Nombre));
                            builder.Append("<ul class=\"dropdown-menu multi-level\" role=\"menu\" aria-labelledby=\"dropdownMenu\">");
                            var html = GetOptionsChildren(options, option.Codigo);
                            if (html != string.Empty)
                            {
                                builder.Append(html);
                                builder.Append("</ul></li>");
                            }
                            else
                            {
                            }
                            break;
                        case 1:
                            builder.Append(string.Format("<li><i class=\"{0}\"></i>&nbsp;&nbsp;<a href=\"{1}\">{2}</a></li>", option.Abreviatura, option.Ruta, option.Nombre));
                            break;
                        default:
                            break;

                    }
                }
                return builder.ToString();
            }
            else
                return Helper.InvokeErrorHTML("No se pudo cargar las opciones del sistema");
        }


    }

    public class Option
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public int CodigoPadre { get; set; }
        public string Nivel { get; set; }
        public int TipoApertura { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public string TipoRuta { get; set; }
        public string Abreviatura { get; set; }
    }

    #endregion
}
