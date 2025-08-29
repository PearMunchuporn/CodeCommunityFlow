using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCommunityFlow.CustomFilter
{
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filter)
        {
            var session = filter.HttpContext.Session;

            // เช็คว่า logged in เป็น user หรือ admin
            bool isLoggedInUser = session["CurrentUserID"] != null;
            bool isLoggedInAdmin = session["CurrentAdminID"] != null;

            if (!isLoggedInUser && !isLoggedInAdmin)
            {
                filter.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Account", action = "Login", area = "" }));
            }
        }
    }
}