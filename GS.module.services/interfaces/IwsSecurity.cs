using GS.module.entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GS.module.services
{
    [ServiceContract]
    public interface IwsSecurity
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UserValidate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UserValidate(string alias, string clave);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetOptions",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetSectionName",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetSectionName();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ClosedSession",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ClosedSession();

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "ValidarMembresia",
        //    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //bool ValidarMembresia(MasterTwo conexion, string[] parametros);

    }
}
