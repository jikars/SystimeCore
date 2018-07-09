using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace ErpDataAccessFromSystime.Erps.DmsV1.DataAcces
{
    /// <summary>
    /// Clase encargada de manejar la logica de usuario 
    /// </summary>
    internal class CustomersDmsV1
    {

        private ParamsContract ParamsContract { get; set; }
        private String ConectionStringErp { get; set; }
        
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        internal CustomersDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
            ConectionStringErp = paramsContract?.ConectionStringErp;
        }

        /// <summary>
        /// Metoto encargado de obtener Customer a partir 
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns></returns>
        internal Customers GetCustomer(String jsonKeys)
        {
            CustomerKeys customerKeys = JsonConvert.DeserializeObject<CustomerKeys>(jsonKeys);
            if (customerKeys != null && decimal.TryParse(customerKeys?.IdCustomer, out decimal nit))
            {
                using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
                {
                    DataBase.Database.CommandTimeout = 1000;
                    terceros customerErp = DataBase.terceros.FirstOrDefault(d => d.nit == nit);
                    return ParseCustomer(customerErp);
                }
            }
            return null;
        }

        /// <summary>
        /// Metoto encargado de convertir un trabajador de dmsv1 to trabajador 
        /// de systime
        /// </summary>
        /// <param name="workerErp"></param>
        /// <returns></returns>
        internal Customers ParseCustomer(terceros customerErp)
        {

            if (customerErp != null)
            {
                //AjusCustomer 
                String[] phones = new ToolsDmsV1(ParamsContract).AjustPhone(customerErp.telefono_2, customerErp.telefono_1);
                Customers customer = new Customers()
                {
                    FullName = customerErp.nombres,
                    Email = customerErp.email2,
                    DocumentNumber = customerErp.nit.ToString(),
                    Phone = phones[1],
                    Mobile = phones[0],
                    IdCountry = new ToolsDmsV1(ParamsContract).AjusCountrie(customerErp.y_pais),
                    IdCity = new ToolsDmsV1(ParamsContract).AjusCity(customerErp.y_ciudad, customerErp.y_dpto, customerErp.y_pais),
                    Address = customerErp.direccion
                };
                return customer;
            }
            return null;
        }

    }

    public  class CustomerKeys
    {
        public String IdCustomer { get; set; }
    }
}