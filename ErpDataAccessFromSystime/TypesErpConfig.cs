using ErpDataAccessFromSystime.Erps.DmsV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystimeDataAcces.DataAccess;

namespace ErpDataAccessFromSystime
{
    public static  class TypesErpConfig
    {

        public enum ErpsTypes
        {
            DMSV1,DMSV2
        }

        private readonly static Dictionary<ErpsTypes, Type> IntanceResolveType = null;


        /// <summary>
        /// Cosntructor estatico
        /// </summary>
       
        static TypesErpConfig()
        {
            if (IntanceResolveType == null)
            {
                IntanceResolveType = new Dictionary<ErpsTypes,Type>
                {
                    {  ErpsTypes.DMSV1 , typeof(DmsV1)}
                };
            }
        }


        /// <summary>
        ///metodo encargado de resolver una instancia 
        ///a partir de un enumerable 
        /// </summary>
        /// <param name="typeErp"></param>
        /// <returns></returns>
       
        internal static T ResolveIntance<T>(ErpsTypes typeErp)
        {

            if (IntanceResolveType.ContainsKey(typeErp))
            {
                Object Obj = Activator.CreateInstance(IntanceResolveType.FirstOrDefault(t => t.Key == typeErp).Value);

                if (Obj is T)
                    return (T)Obj;
            }
            return default(T);
        }



    }
}
