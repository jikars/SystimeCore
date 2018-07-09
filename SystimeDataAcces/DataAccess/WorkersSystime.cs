using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystimeDataAcces.DataAccess
{
    public class WorkersSystime
    {

        /// <summary>
        /// Cadena de coneccion a la base de datos 
        /// </summary>
        private string ConectionString { get; set; }

        /// <summary>
        /// Cosntructor de la clase 
        /// </summary>
        public WorkersSystime(String conecctionString)
        {
            ConectionString = conecctionString;
        }

        /// <summary>
        /// Metoto encargado de guarrar un trabajador en systime 
        /// considerando el cargo al que pertenece 
        /// </summary>
        /// <param name="entityWorker"></param>
        /// <returns></returns>
        public Workers SaveWorker(Workers entityWorker, String userModify)
        {


            if (entityWorker != null)
            {

                ////Guarda el job title y obtiene el id 
                //if (entityWorker.IdJobTitle == ConstantsSystimeDataAccess.ID_JOB_TITLE_DEFAULT)
                //    entityWorker.IdJobTitle = ConstantsSystimeDataAccess.ID_JOB_TITLE_DEFAULT;

                using (SystimedbEntities Systimedb = new SystimedbEntities(ConectionString))
                {
                    Workers entitieCurrent = Systimedb.Workers.FirstOrDefault(I => I.IdWorker == entityWorker.IdWorker);

                    if (entitieCurrent != null)
                        return entitieCurrent;
                    else
                    {
                        entityWorker.CreatedAt = DateTime.Now;
                        entityWorker.CreatedById = userModify;
                        Systimedb.Workers.Add(entityWorker);

                        if (Systimedb.SaveChanges() > 0)
                           return entityWorker;



                    }
                }
            }
            return null;
        }
    }
}
