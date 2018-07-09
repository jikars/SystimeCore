using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.DataAccess;

namespace SystimeDataAcces.DataAccess
{
    public class VehiclesSystime
    {
        /// <summary>
        /// CCadena de conecccion contra la bse de datos
        /// </summary>
        private string ConectionString { get; set; }


        //Constructor de la clase
        public VehiclesSystime(String conectionString)
        {
            ConectionString = conectionString;
        }


        /// <summary>
        /// Metoto encargado de consultar un vehiculo sobre la base de datos
        /// de systiem por su id sea el vin o la placa 
        /// </summary>
        /// <param name="vinVehicleOrId">id o vin del vehiculo</param>
        /// <returns>entidad de vehiculo</returns>
        public Vehicles GetVehicleFromIdVin(String vinVehicleOrId)
        {
            if (!String.IsNullOrEmpty(vinVehicleOrId))
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    return Systimedb.Vehicles.FirstOrDefault(v => v.IdVinNumber == vinVehicleOrId);
                }
            }
            return null;
        }


        /// <summary>
        /// Metoto encargado de guardar o actualizar un vehiculo 
        /// en la base de datos de systime 
        /// </summary>
        /// <param name="entityVehicle"></param>
        /// <param name="userModify"></param>
        /// <returns></returns>
        public Vehicles SaveVehicle(Vehicles entityVehicle, String userModify)
        {
            Vehicles currentVehicle = null;
            Boolean changeUpdateEntity = false;
            if (entityVehicle != null)
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    List<Vehicles> entitiesCurrentVehicles = Systimedb.Vehicles.Where(v => v.Plate == entityVehicle.Plate).ToList();
                    if(entitiesCurrentVehicles == null || entitiesCurrentVehicles.Count == 0)
                        entitiesCurrentVehicles = Systimedb.Vehicles.Where(v => v.IdVinNumber == entityVehicle.IdVinNumber).ToList();
                    if (entitiesCurrentVehicles.Count > 0)
                    {
                        if (entitiesCurrentVehicles.Count == 1)
                            currentVehicle = entitiesCurrentVehicles[0];
                        else if (entitiesCurrentVehicles.Count > 1)
                            currentVehicle = entitiesCurrentVehicles.FirstOrDefault(v => v.IdCity == entityVehicle.IdCity);

                        if (currentVehicle != null)
                        {
                            //Comienza el proceso de actualizacion 

                            //Entity framwwork comienza a hacer seguimiento a la entidad
                            Systimedb.Vehicles.Attach(currentVehicle);
                            //Cambia el etado a modificado
                            currentVehicle.Color = UtilsDataAcces.ValidateDiferentString(currentVehicle.Color, entityVehicle.Color, false, changeUpdateEntity, out changeUpdateEntity);

                            currentVehicle.DealerVehicleModel = UtilsDataAcces.ValidateDiferentString(currentVehicle.DealerVehicleModel, entityVehicle.DealerVehicleModel, true, changeUpdateEntity, out changeUpdateEntity);

                            currentVehicle.IdCity = (UtilsDataAcces.ValidateDiferentInt(currentVehicle.IdCity, entityVehicle.IdCity, false, changeUpdateEntity, out changeUpdateEntity)) ?? 0;

                            currentVehicle.IdInsuranceCompany = UtilsDataAcces.ValidateDiferentInt(currentVehicle.IdInsuranceCompany, entityVehicle.IdInsuranceCompany, true, changeUpdateEntity, out changeUpdateEntity);

                            currentVehicle.IdVehicleModel = (UtilsDataAcces.ValidateDiferentInt(currentVehicle.IdVehicleModel, entityVehicle.IdVehicleModel, false, changeUpdateEntity, out changeUpdateEntity)) ?? 0;

                            currentVehicle.IdVehicleServiceType = (UtilsDataAcces.ValidateDiferentInt(currentVehicle.IdVehicleServiceType, entityVehicle.IdVehicleServiceType, false, changeUpdateEntity, out changeUpdateEntity));

                            currentVehicle.IdVehicleType = (UtilsDataAcces.ValidateDiferentInt(currentVehicle.IdVehicleType, entityVehicle.IdVehicleType, false, changeUpdateEntity, out changeUpdateEntity));

                            if (currentVehicle.LastMileage != entityVehicle.LastMileage && entityVehicle.LastMileage != null && currentVehicle.LastMileage < entityVehicle.LastMileage)
                            {
                                currentVehicle.LastMileage = entityVehicle.LastMileage;
                                changeUpdateEntity = true;
                            }

                            currentVehicle.ModelYear = (UtilsDataAcces.ValidateDiferentInt(currentVehicle.ModelYear, entityVehicle.ModelYear, false, changeUpdateEntity, out changeUpdateEntity)).Value;

                            currentVehicle.Plate = UtilsDataAcces.ValidateDiferentString(currentVehicle.Plate, entityVehicle.Plate, false, changeUpdateEntity, out changeUpdateEntity);

                            currentVehicle.DealerCity = UtilsDataAcces.ValidateDiferentString(currentVehicle.DealerCity, entityVehicle.DealerCity, true, changeUpdateEntity, out changeUpdateEntity);

                            currentVehicle.IdCustomerOwner = (UtilsDataAcces.ValidateDiferentDecimal(currentVehicle.IdCustomerOwner, entityVehicle.IdCustomerOwner, true, changeUpdateEntity, out changeUpdateEntity)).Value;

                            currentVehicle.SaleGuaranteeAt = UtilsDataAcces.ValidateDiferentDateTime(currentVehicle.SaleGuaranteeAt, entityVehicle.SaleGuaranteeAt, changeUpdateEntity, out changeUpdateEntity);

                            currentVehicle.GuaranteedSaleDistanceTraveled = UtilsDataAcces.ValidateDiferentInt(currentVehicle.GuaranteedSaleDistanceTraveled, entityVehicle.GuaranteedSaleDistanceTraveled, false,changeUpdateEntity, out changeUpdateEntity);

                            if (changeUpdateEntity)
                            {

                                currentVehicle.UpdatedAt = DateTime.Now;
                                currentVehicle.UpdatedById = userModify;
                                Systimedb.Entry(currentVehicle).State = EntityState.Modified;
                                Systimedb.SaveChanges();
                            }
                            return currentVehicle;
                        }
                    }
                    else
                    {
                        entityVehicle.CreatedAt = DateTime.Now;
                        entityVehicle.CreatedById = userModify;
                        Systimedb.Vehicles.Add(entityVehicle);
                        if (Systimedb.SaveChanges() > 0)
                            return entityVehicle;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Metoto encargado de actualizar si un vehiculo tiene o no fotografia 
        /// </summary>
        /// <param name="idVinVehilce">id del vehiculo</param>
        /// <param name="hasPhoto">si tiene fotografia o no</param>
        /// <returns></returns>
        public Boolean UpdateHasPhoto(String idVinVehilce, Boolean hasPhoto)
        {
            if (!String.IsNullOrEmpty(idVinVehilce))
            {
                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    Vehicles vehicles = Systimedb.Vehicles.FirstOrDefault(v => v.IdVinNumber == idVinVehilce);
                    if (vehicles != null)
                    {
                        vehicles.HasPhoto = hasPhoto;
                        if (Systimedb.SaveChanges() > 0)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
