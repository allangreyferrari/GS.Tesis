namespace GS.module.services
{
    using entities.Base;
    using entities.Primary;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsCommon" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsCommon
    {
        #region String
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringTwo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringTwoCollection ListarStringTwo(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringFive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringFiveCollection ListarStringFive(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringTen", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringTenCollection ListarStringTen(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringTwenty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringTwentyCollection ListarStringTwenty(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringThirty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringThirtyCollection ListarStringThirty(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringFifty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringFiftyCollection ListarStringFifty(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringSeventy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringSeventyCollection ListarStringSeventy(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarStringHundred", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringHundredCollection ListarStringHundred(string key, object[] parametros, object[] cryp = null);
        #endregion

        #region Basic
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicTwo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicTwoCollection ListarBasicTwo(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicFive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicFiveCollection ListarBasicFive(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicTen", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicTenCollection ListarBasicTen(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicTwenty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicTwentyCollection ListarBasicTwenty(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicThirty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicThirtyCollection ListarBasicThirty(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicFifty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicFiftyCollection ListarBasicFifty(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicSeventy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicSeventyCollection ListarBasicSeventy(string key, object[] parametros, object[] cryp = null);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarBasicHundred", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BasicHundredCollection ListarBasicHundred(string key, object[] parametros, object[] cryp = null);
        #endregion

        #region Session
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ObtenerSessionID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string ObtenerSessionID();
        #endregion

        #region Transaction
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EjecutarTransaction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Transaction EjecutarTransaction(string key, object[] parametros, object[] cryp = null);
        #endregion


    }
}
