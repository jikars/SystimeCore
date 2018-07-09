using ErpDataAccessFromSystime;
using ErpDataAccessFromSystime.Contract;
using ErpDataAccessFromSystime.Contract.ErpDataAccessFromSystime.Contract;
using ServicesAccessUbicar.cs;
using ServicesAccessUbicar.cs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace IntegrateErpToSystime
{
    internal static class DependencyFactory
    {
        private static IUnityContainer container;

        /// <summary>
        /// REfrencia puublica al contenerdor de depdencencias
        /// </summary>
        internal static IUnityContainer Container
        {
            get
            {
                return container;
            }
            private set
            {
                container = value;
            }
        }

        /// <summary>
        /// Constructor estatico y donde se iniciliza el contendor
        /// </summary>
        static DependencyFactory()
        {
            if (Container == null)
            {
                Container = new UnityContainer();
                PrechargeDependency();
            }
        }

        /// <summary>
        /// Meetodo encargado de resolver una dependecia segun el tipo 
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            T ret = default(T);
            if (Container.IsRegistered(typeof(T)))
                ret = Container.Resolve<T>();
            return ret;
        }


        /// <summary>
        /// Metoto encargao de resolver una instancia que no ha sido
        /// registrada en el contedor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ResolveInstance<T>()
        {
            T ret = default(T);
            if (!Container.IsRegistered(typeof(T)))
                ret = Container.Resolve<T>();
            return ret;
        }



        /// <summary>
        /// Metoto encargado de registrar una dependencia
        /// dentro de el contenedor
        /// o de actualizar una 
        /// </summary>
        /// <typeparam name="tFrom"></typeparam>
        /// <typeparam name="tTo"></typeparam>
        public static void RegisterDependency<tFrom, tTo>() where tTo : tFrom
        {
            Container.RegisterType(typeof(tFrom), typeof(tTo));
        }


        /// <summary>
        /// Hace un registro de las dependencias que se va n a uysar
        /// </summary>
        /// 
        private static void PrechargeDependency()
        {
            RegisterDependency<IDataAccesErpContract, DataAccesErpContract> ();
            RegisterDependency<IServiceClientUbicar, RequestUbicarAccess>();
        }

    }
}
