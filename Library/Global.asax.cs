﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Library.Services;
using System.Diagnostics;
using Library.Configurations;
using Library.Controllers;

namespace Library
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Debug.WriteLine(DateTime.Now.ToShortTimeString() + " Application_Start");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(ControllerFactoryHelper.GetControllerFactory());

            // init repositoroes HERE if we are not using session_cache for storage

            if (!Configuration.usingSessionCashe)
            {
                RepositoriesInit.Init();
            }
        }


        protected void Session_Start(Object sender, EventArgs e)
        {
            Debug.WriteLine(DateTime.Now.ToShortTimeString() + " Start_Session");

            // init repositoroes HERE if we are using session_cache for storage

            initSession();
        }


        public static void initSession() 
        {
            if (Configuration.usingSessionCashe)
            {
                Models.State.StartSession();
                RepositoriesInit.Init();
            }
        }
    }
}