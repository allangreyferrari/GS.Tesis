using GS.module.entities.Base;
using GS.module.entities.IT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GS.module.services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsConfiguracion" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsConfiguracion
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarSistemas", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SistemaCollection ListarSistemas(Sistema oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarSistemasCombo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SistemaCollection ListarSistemasCombo(Sistema oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "InsertarSistema", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Transaction InsertarSistema(Sistema oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditarSistema", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Transaction EditarSistema(Sistema oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarNodos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        NodoCollection ListarNodos(Nodo oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "InsertarNodo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Transaction InsertarNodo(Nodo oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditarNodo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Transaction EditarNodo(Nodo oe);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListarTipoNodo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterTwoCollection ListarTipoNodo();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ReciclarPool", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Transaction ReciclarPool(string id, string namepool);
    }
}
