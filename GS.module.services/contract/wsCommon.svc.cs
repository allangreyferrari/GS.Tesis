namespace GS.module.services
{
    using entities.Base;
    using entities.Primary;
    using logic.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Web;

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
        #endregion
        ~wsCommon() { }
    }
}
