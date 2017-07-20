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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsMaster" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsMaster
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarOperacionesTesoreria", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterCollection ListarOperacionesTesoreria(string bd, string orden, string fechaini, string fechafin, int check1, int check2);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarOperacionesTesoreriaEV", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterCollection ListarOperacionesTesoreriaEV(string bd, string orden, string fechaini, string fechafin, int check1, int check2);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarOperacionesTesoreriaALL", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterCollection ListarOperacionesTesoreriaALL(string bd, string documento, string orden, string fechaini, string fechafin, int check1, int check2);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AprobarOperacionesTesoreria", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AprobarOperacionesTesoreria(string Op, string Bd, string Clave);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarEmpresas", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterCollection ListarEmpresas();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarEstados", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterCollection ListarEstados();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarDocumentos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterCollection ListarDocumentos();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ObtieneRangoBusquedaAprobacion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string[] ObtieneRangoBusquedaAprobacion();

    }
}
