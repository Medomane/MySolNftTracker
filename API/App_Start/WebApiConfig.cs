using MyLibrary;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Reflection;
using System.Web.Http.Cors;
using MySolNftTracker.Models;

namespace MySolNftTracker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API configuration and services
            config.Filters.Add(new BasicAuthenticationAttribute());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }

    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null) actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            else
            {
                try
                {
                    var usernamePasswordArray = _encode.Base64Decode(actionContext.Request.Headers.Authorization.Parameter).Split(':');

                    _app.Init(Assembly.GetExecutingAssembly());
                    var user = Models.AppUser.Login(usernamePasswordArray[0], usernamePasswordArray[1]);
                    Models.AppUser.Current = user;

                    IPrincipal principal = new GenericPrincipal(new GenericIdentity(user.Username), new[] { user.Role == UserRole.Admin ? "ADMIN" : "USER" });
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null) HttpContext.Current.User = principal;
                }
                catch (Exception ex)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message.Replace(Environment.NewLine, "") + ".");
                    ex.Log();
                }
            }
        }
    }
}
