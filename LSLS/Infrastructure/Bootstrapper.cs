using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSLS.Repository;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace LSLS.Infrastructure
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit  
            container.RegisterType<IStaffRepository, StaffRepository>();
            container.RegisterType<ITruckDriverRepository, TruckDriverRepository>();
            container.RegisterType<ITruckLocationRepository, TruckLocationRepository>();
            container.RegisterType<IAuthProvider, FormsAuthProvider>();


            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}