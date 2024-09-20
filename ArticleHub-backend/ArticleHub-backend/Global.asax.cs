using ArticleHub_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ArticleHub_backend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAdminUser();
        }

        private void InitializeAdminUser()
        {
            Console.WriteLine("test");
            using (var db = new ArticleHubEntities())
            {
                // Vérifiez s'il y a des utilisateurs dans la base de données
                if (!db.Appusers.Any())
                {
                    // Si aucun utilisateur n'existe, créer un utilisateur admin par défaut
                    var adminUser = new Appuser
                    {
                        name = "Admin",
                        email = "admin@gmail.com", // Mettez l'email admin par défaut
                        password = "123", // Mettez un mot de passe sécurisé par défaut
                        status = "true",
                        isDeletable = "false" // Assurez-vous que cet utilisateur ne peut pas être supprimé
                    };

                    db.Appusers.Add(adminUser);
                    db.SaveChanges();
                }
            }
        }

    }
}
