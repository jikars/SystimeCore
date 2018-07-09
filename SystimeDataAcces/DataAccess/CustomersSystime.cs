using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DataAccess;

namespace SystimeDataAcces.DataAccess
{
    /// <summary>
    /// Clase encargada de manejar la logica de clientes contra la base de datos de systime 
    /// </summary>
    public class CustomersSystime
    {

        /// <summary>
        /// Cadena de coneccion contra la bse de datos
        /// </summary>
        private string ConectionString { get; set; }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public CustomersSystime(String conectionString)
        {
            ConectionString = conectionString;
        }


        /// <summary>
        /// Metoto encargado de grabar o actualizar un entitdad de tipo Customer
        /// en la base de datos de systime
        /// </summary>
        /// <param name="entityCustomers">entidad customer</param>
        /// <returns>id en la base de datos asignado a customer</returns>
        public Customers SaveCustomer(Customers entityCustomers, String userModify)
        {
            Boolean changeUpdateEntity = false;
            if (entityCustomers != null)
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    List<Customers> entitiesCurrent = Systimedb.Customers.Where(c => c.DocumentNumber == entityCustomers.DocumentNumber && c.IdCountry == entityCustomers.IdCountry).ToList();
                    Customers entitieCurrent = null;
                    if (entitiesCurrent.Count > 0)
                    {

                        if (entitiesCurrent.Count == 1)
                            entitieCurrent = entitiesCurrent[0];
                        else if (entitiesCurrent.Count > 1)
                            entitieCurrent = entitiesCurrent.FirstOrDefault(c => c.IdCountry == entityCustomers.IdCountry);

                        if (entitieCurrent != null)
                        {
                            //Entity framwwork comienza a hacer seguimiento a la entidad
                            Systimedb.Customers.Attach(entitieCurrent);
                            //Cambia el etado a modificado

                            entitieCurrent.IdCountry = UtilsDataAcces.ValidateDiferentString(entitieCurrent.IdCountry, entityCustomers.IdCountry, true, changeUpdateEntity, out changeUpdateEntity);

                            entitieCurrent.IdCity = (UtilsDataAcces.ValidateDiferentInt(entitieCurrent.IdCity, entityCustomers.IdCity, false, changeUpdateEntity, out changeUpdateEntity)).Value;

                            entitieCurrent.Email = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Email, entityCustomers.Email, false, changeUpdateEntity, out changeUpdateEntity);

                            entitieCurrent.FullName = UtilsDataAcces.ValidateDiferentString(entitieCurrent.FullName, entityCustomers.FullName, false, changeUpdateEntity, out changeUpdateEntity);

                            entitieCurrent.DocumentNumber = UtilsDataAcces.ValidateDiferentString(entitieCurrent.DocumentNumber, entityCustomers.DocumentNumber, false, changeUpdateEntity, out changeUpdateEntity);

                            entitieCurrent.Mobile = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Mobile, entityCustomers.Mobile, false, changeUpdateEntity, out changeUpdateEntity);

                            entitieCurrent.Phone = UtilsDataAcces.ValidateDiferentString(entitieCurrent.Phone, entityCustomers.Phone, false, changeUpdateEntity, out changeUpdateEntity);


                            if (changeUpdateEntity)
                            {
                                Systimedb.Entry(entitieCurrent).State = EntityState.Modified;
                                entitieCurrent.UpdatedAt = DateTime.Now;
                                Systimedb.SaveChanges();
                            }
                            return entitieCurrent;
                        }
                    }
                    else
                    {
                        entityCustomers.CreatedById = userModify;
                        entityCustomers.CreatedAt = DateTime.Now;
                        Systimedb.Customers.Add(entityCustomers);

                        if (Systimedb.SaveChanges() > 0)
                            return entityCustomers;
                    }
                }
            }
            return null;
        }
    }
}
