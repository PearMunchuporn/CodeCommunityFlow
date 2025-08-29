using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using CodeCommunityFlow.App_Start;
using System.Web.Optimization;
using System.Data.Entity;
using CodeCommunityFlow.DomainModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Unity;
using CodeCommunityFlow.ServiceLayers.ProfilesMapping;
namespace CodeCommunityFlow
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
         

     
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<CodeCommunityFlowDbContext>(null);
            // AutoMapper global configuration

        
           
       
        }
    }
}