using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DEMOMVCC.Security
{
    public class CustomException : FilterAttribute,
IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && (filterContext.Exception is NullReferenceException || filterContext.Exception is InvalidOperationException))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Home", action = "Index" }));
                filterContext.ExceptionHandled = true;
            }
        }

    }
}