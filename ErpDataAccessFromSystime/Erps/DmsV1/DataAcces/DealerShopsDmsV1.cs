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
    /// Clase encargada de la logica dealer shop para el erp
    /// </summary>
    internal class DealerShopsDmsV1
    {

        private ParamsContract ParamsContract { get; set; }
        private String ConectionStringErp { get; set; }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        internal DealerShopsDmsV1(ParamsContract paramsContract)
        {
            ParamsContract = paramsContract;
            ConectionStringErp = paramsContract?.ConectionStringErp;

        }

        /// <summary>
        /// Meotodo encargado de consultar la bodega del erp
        /// </summary>
        /// <param name="idShop"></param>
        /// <returns></returns>
        internal CloudCatalogDealerShops GetDealerShop(String jsonKeys)
        {
            DelaerShopKeysDmsV1 DealerShopKeys = JsonConvert.DeserializeObject<DelaerShopKeysDmsV1>(jsonKeys);
            if (DealerShopKeys!= null && int.TryParse(DealerShopKeys?.IdShop, out int id))
            {
                using (DmsV1Entities DataBase = new DmsV1Entities(ConectionStringErp))
                {
                    DataBase.Database.CommandTimeout = 1000;
                    bodegas dealerShopErp = DataBase.bodegas.FirstOrDefault(b => b.id == id);
                    if (dealerShopErp != null)
                        return ParseDealerShop(dealerShopErp);
                }
            }
            return null;
        }


        /// <summary>
        /// Metoto encargado de convertir la bodeaga de erp a bodega systime
        /// </summary>
        /// <param name="dealerShopErp"></param>
        /// <returns></returns>
        internal CloudCatalogDealerShops ParseDealerShop(bodegas dealerShopErp)
        {
            if (dealerShopErp != null)
            {
                String[] phones = new ToolsDmsV1(ParamsContract).AjustPhone(dealerShopErp.telefono, null);
                CloudCatalogDealerShops dealerShop = new CloudCatalogDealerShops()
                {

                    Shop = dealerShopErp.descripcion,
                    Address = dealerShopErp.direccion,
                    IdCity = new ToolsDmsV1(ParamsContract).AjusCity(dealerShopErp.ciudad, dealerShopErp.departamento, dealerShopErp.ciudad),
                    Phone = phones[1],
                    IdDealerShop = dealerShopErp.id.ToString(),
                    Mobile = phones[0]
                };
                return dealerShop;
            }
            return null;
        }
    }

    public class DelaerShopKeysDmsV1
    {
        public String  IdShop { get; set; }
    }
}
