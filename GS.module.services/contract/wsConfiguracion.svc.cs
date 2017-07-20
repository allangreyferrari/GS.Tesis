using GS.module.entities.Base;
using GS.module.entities.IT;
using GS.module.logic.IT;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    public class wsConfiguracion : IwsConfiguracion
    {
        #region Sistemas Produccion
        public SistemaCollection ListarSistemas(Sistema oe)
        {
            blSistema bl = new blSistema();
            Transaction transaction = Helper.InitTransaction();
            return bl.ListarSistemas(oe, out transaction);
        }

        public SistemaCollection ListarSistemasCombo(Sistema oe)
        {
            blSistema bl = new blSistema();
            Transaction transaction = Helper.InitTransaction();
            SistemaCollection ocol = bl.ListarSistemas(oe, out transaction);
            ocol.Rows.Insert(0, new Sistema() { id = 0, nombre = " [ SELECCIONE ] ", descripcion = "", estado = true, key = "" });
            return ocol;
        }

        public Transaction InsertarSistema(Sistema oe)
        {
            blSistema bl = new blSistema();
            Transaction transaction = Helper.InitTransaction();
            bl.InsertarSistema(oe, out transaction);
            return transaction;
        }

        public Transaction EditarSistema(Sistema oe)
        {
            blSistema bl = new blSistema();
            Transaction transaction = Helper.InitTransaction();
            bl.EditarSistema(oe, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                try
                {
                    bl = new blSistema();
                    Transaction transactionLIST = Helper.InitTransaction();
                    foreach (Sistema sistema in bl.ListarSistemas(new Sistema()
                    {
                        id = oe.id,
                        nombre = "",
                        descripcion = "",
                        key = "",
                        poolname = "",
                        estado = false
                    }, out transactionLIST).Rows)
                    {
                        //transaction.message = transaction.message + "\nEntro al Foreach";

                        System.Configuration.ExeConfigurationFileMap wcfm = new System.Configuration.ExeConfigurationFileMap
                        {
                            ExeConfigFilename = oe.fileconfig
                        };
                        //transaction.message = transaction.message + "\nArchivo encontrado";
                        System.Configuration.Configuration fileconfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(wcfm, System.Configuration.ConfigurationUserLevel.None);
                        //transaction.message = transaction.message + "\nInicializa";
                        fileconfig.AppSettings.Settings.Remove("sistema");
                        fileconfig.AppSettings.Settings.Remove("key");
                        //transaction.message = transaction.message + "\nRemove";
                        fileconfig.AppSettings.Settings.Add("sistema", oe.sistema_encriptado);
                        fileconfig.AppSettings.Settings.Add("key", oe.key_encriptado);
                        //transaction.message = transaction.message + "\nAdd";
                        fileconfig.Save(System.Configuration.ConfigurationSaveMode.Modified);
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                        //transaction.message = transaction.message + "\nGuardo";
                        transaction.message = transaction.message + "\nSe actualizó el archivo de configuración del sistema";
                        fileconfig = null;
                    }
                }
                catch (Exception ex)
                {
                    transaction.message = transaction.message + "\nNo se pudo actualizar el archivo de configuración: " + ex.Message;
                }
            }
            return transaction;
        }
        #endregion

        public NodoCollection ListarNodos(Nodo oe)
        {
            blNodo bl = new blNodo();
            Transaction transaction = Helper.InitTransaction();
            return bl.ListarNodos(oe, out transaction);
        }

        public Transaction InsertarNodo(Nodo oe)
        {
            blNodo bl = new blNodo();
            Transaction transaction = Helper.InitTransaction();
            bl.InsertarNodo(oe, out transaction);
            return transaction;
        }

        public Transaction EditarNodo(Nodo oe)
        {
            blNodo bl = new blNodo();
            Transaction transaction = Helper.InitTransaction();
            bl.EditarNodo(oe, out transaction);
            return transaction;
        }

        public MasterTwoCollection ListarTipoNodo()
        {
            List<MasterTwo> ocol = new List<MasterTwo>();
            ocol.Add(new MasterTwo { v01 = "0", v02 = " [ TODOS ] " });
            ocol.Add(new MasterTwo { v01 = "1", v02 = " CONEXION " });
            ocol.Add(new MasterTwo { v01 = "2", v02 = " CONSTANTE " });
            return new MasterTwoCollection(ocol, Helper.GetTransaction(TypeTransaction.OK, ""));
        }

        public Transaction ReciclarPool(string id, string namepool)
        {
            Transaction transaction = Helper.InitTransaction();
            string machineName = "LOCALHOST";
            string error = null;
            DirectoryEntry root = null;
            try
            {
                root = new DirectoryEntry("IIS://" + machineName + "/W3SVC/AppPools/" + namepool);
                root.Invoke("Recycle");
                transaction.type = TypeTransaction.OK;
                transaction.message = "El AppPool: \"" + namepool + "\" fue reciclado satisfactoriamente";
            }
            catch (Exception ex)
            {
                transaction.type = TypeTransaction.ERR;
                transaction.message = "No es posible acceder al AppPool: \"" + namepool + "\"\n" + ex.Message;
            }
            return transaction;
        }

    }
}
