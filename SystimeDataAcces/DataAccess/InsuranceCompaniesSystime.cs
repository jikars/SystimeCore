using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DataAccess;

namespace SystimeDataAcces.DataAccess
{
    public class InsuranceCompaniesSystime
    {


        /// <summary>
        /// Cadena de coneccion contra la base de datos
        /// </summary>
        private string ConectionString { get; set; }


        /// <summary>
        /// Consturctor de la clase
        /// </summary>
        public InsuranceCompaniesSystime(String conectionString)
        {
            ConectionString = conectionString;
        }

        /// <summary>
        /// Metoto encargado de grabar o actualizar un registro de la base de
        /// datos 
        /// </summary>
        /// <param name="entityInsurancecompany"></param>
        /// <returns></returns>
        public CloudCatalogInsuranceCompanies SaveInsuranceCompay(CloudCatalogInsuranceCompanies entityInsurancecompany)
        {
            Boolean changeUpdateEntity = false;

            if (entityInsurancecompany != null)
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    CloudCatalogInsuranceCompanies entitieCurrent = Systimedb.CloudCatalogInsuranceCompanies.FirstOrDefault(I => I.IdInsuranceCompany == entityInsurancecompany.IdInsuranceCompany);
                    if (entitieCurrent == null)
                        entitieCurrent = Systimedb.CloudCatalogInsuranceCompanies.FirstOrDefault(I => I.IdCountry == entityInsurancecompany.IdCountry && I.TIN == entityInsurancecompany.TIN);


                    if (entitieCurrent != null)
                    {
                        entitieCurrent.Address = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Address, entityInsurancecompany.Address, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Email = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Email, entityInsurancecompany.Email, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.InsuranceCompany = UtilsDataAcces.ValidateDiferentString(entitieCurrent.InsuranceCompany, entityInsurancecompany.InsuranceCompany, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Mobile = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Mobile, entityInsurancecompany.Mobile, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Phone = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Phone, entityInsurancecompany.Phone, false, changeUpdateEntity, out changeUpdateEntity);

                        entitieCurrent.Website = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Website, entityInsurancecompany.Website, false, changeUpdateEntity, out changeUpdateEntity);

                        if (changeUpdateEntity)
                        {
                            entitieCurrent.UpdatedAt = DateTime.Now;
                            Systimedb.SaveChanges();
                        }

                        return entitieCurrent;
                    }
                    else
                    {
                        entityInsurancecompany.CreatedAt = DateTime.Now;
                        Systimedb.CloudCatalogInsuranceCompanies.Add(entityInsurancecompany);
                        if (Systimedb.SaveChanges() > 0)
                            return entityInsurancecompany;
                    }
                }
            }

            return null;
        }

    }
}
