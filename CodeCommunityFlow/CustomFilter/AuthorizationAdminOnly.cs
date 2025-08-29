using CodeCommunityFlow.ServiceLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCommunityFlow.CustomFilter
{
    public class AuthorizationAdminOnly : FilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filter)
        {
            var session = filter.HttpContext.Session;
            var isAdminSessionSet = session["CurrentUserIsAdmin"] != null;
            var isAdmin = isAdminSessionSet && Convert.ToBoolean(session["CurrentUserIsAdmin"]);

            var currentArea = filter.RouteData.DataTokens["area"] as string;
            var currentController = filter.ActionDescriptor.ControllerDescriptor.ControllerName;
            var currentAction = filter.ActionDescriptor.ActionName;

            // ถ้าไม่ใช่ admin แต่พยายามเข้า admin area
            if (!isAdmin && string.Equals(currentArea, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                // ยกเว้นหน้า Login เพื่อหลีกเลี่ยงลูป
                if (!(currentController == "Account" && currentAction == "Login"))
                {
                    filter.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new { controller = "Account", action = "Login", area = "" }));
                }
            }
        }
    }
}