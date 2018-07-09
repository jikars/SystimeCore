using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DataAccess;

namespace SystimeDataAcces.DataAccess
{
    public class DealerShopsSystime
    {

        /// <summary>
        /// Cadena de coneccion contra la bse de datos
        /// </summary>
        private string ConectionString { get; set; }

        /// <summary>
        /// Constructor de clase
        /// </summary>
        public DealerShopsSystime(String conectionString)
        {
            ConectionString = conectionString;
        }

        /// <summary>
        /// MEtoto encargado de guardar el dealer shop o actualizar en la base 
        /// de datos de systime 
        /// </summary>
        /// <param name="entityDealerShop"></param>
        /// <returns></returns>
        public CloudCatalogDealerShops SaveDealerShop(CloudCatalogDealerShops entityDealerShop)
        {
            Boolean changeUpdateEntity = false;
            if (entityDealerShop != null)
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    CloudCatalogDealerShops entitieCurrent = Systimedb.CloudCatalogDealerShops.FirstOrDefault(I => I.IdDealerShop == entityDealerShop.IdDealerShop);
                    if (entitieCurrent != null)
                    {

                        entitieCurrent.Address = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Address, entityDealerShop.Address, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Email = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Email, entityDealerShop.Email, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.IdCity = (UtilsDataAcces.ValidateDiferentInt(entitieCurrent.IdCity, entityDealerShop.IdCity, false, changeUpdateEntity, out changeUpdateEntity)).Value;

                        entitieCurrent.Latitude = (UtilsDataAcces.ValidateDiferentDouble(entitieCurrent.Latitude, entityDealerShop.Latitude, false, changeUpdateEntity, out changeUpdateEntity)) ?? 0;

                        entitieCurrent.Longitude = (UtilsDataAcces.ValidateDiferentDouble(entitieCurrent.Longitude, entityDealerShop.Longitude, false, changeUpdateEntity, out changeUpdateEntity)) ?? 0;

                        entitieCurrent.Mobile = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Mobile, entityDealerShop.Mobile, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Phone = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Phone, entityDealerShop.Phone, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Shop = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Shop, entityDealerShop.Shop, true, changeUpdateEntity, out changeUpdateEntity);

                        if (changeUpdateEntity)
                        {
                            entitieCurrent.UpdatedAt = DateTime.Now;
                            Systimedb.SaveChanges();
                        }

                        return entitieCurrent;
                    }
                    else
                    {
                        entityDealerShop.CreatedAt = DateTime.Now;
                        Systimedb.CloudCatalogDealerShops.Add(entityDealerShop);
                        if (Systimedb.SaveChanges() > 0)
                            return entityDealerShop;
                    }
                }
            }
            return null;
        }
    }
}
