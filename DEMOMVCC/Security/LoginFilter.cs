using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace DEMOMVCC.Security
{
    public class LoginFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {


            if (string.IsNullOrEmpty((string)HttpContext.Current.Session["USER_LOGIN_NAME"]))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Home", action = "Login" }));
            }
            else
            {
                Int32 session = (Int32)HttpContext.Current.Session["USER_LOGIN"];
                var role = Roles.Split(new char[] { ',' });
                if (!role.Contains(session.ToString()))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Home", action = "Login" }));
                }
            }

        }

      
    }
}