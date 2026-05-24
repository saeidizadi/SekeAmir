using Application.Contracts.Users;
using Domain;
using Domain.Account.Permission;
using Persistence.Extention;
using SekeAmir.Web.MiddleWare;

namespace SekeAmir.Web.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PermissionCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PermissionCheckMiddleware> _logger;
        public PermissionCheckMiddleware(RequestDelegate next, ILogger<PermissionCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        
        {
            var endpoint = httpContext.GetEndpoint();
            if (endpoint == null)
            {
                await _next(httpContext);
                return;
            }

            // اگر روی اکشن یا کنترلر صفت [AllowAnonymous] زده شده بود، عبور کن
            var allowAnonymous = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>();
            if (allowAnonymous != null)
            {
                await _next(httpContext);
                return;

            }
            var User = httpContext.User;
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogWarning(EventIdList.Error,
                    "⚠️ کاربر ناشناس تلاش دسترسی داشت -> Path={Path}, Method={Method}, IP={IP}",
                    httpContext.Request.Path,
                    httpContext.Request.Method,
                    httpContext.Connection.RemoteIpAddress?.ToString()
                ); httpContext.Response.Redirect("/Login");
                return;
            }

            var area = httpContext.GetRouteValue("area")?.ToString();
            var controller = httpContext.GetRouteValue("controller")?.ToString();
            var action = httpContext.GetRouteValue("action")?.ToString();
            var permissionChecker = httpContext.RequestServices.GetRequiredService<IPermisionList>();





            var permissions = httpContext.Session.GetData<IEnumerable<PermissionList>>("UserPermission");

            if (permissions == null)
            {
                permissions = await permissionChecker.GetPermissionOfUserAsync(httpContext.User.GetUserId());
                httpContext.Session.SetData("UserPermission", permissions);
            }
            if (permissions == null)
            {
                _logger.LogWarning(EventIdList.Error,
    "⚠️ کاربر {UserId} ({UserName}) دسترسی غیرمجاز -> Area={Area}, Controller={Controller}, Action={Action}, Path={Path}",
    User.GetUserId(),
    User.Identity.Name,
    area ?? "-",
    controller ?? "-",
    action ?? "-",
    httpContext.Request.Path
);
                httpContext.Response.Redirect("/AccessDenied");

                return;
            }
            var obj = permissions.Any(p =>
                (p.Area == area && p.ControllerName == null && p.ActionName == null) ||
                (p.Area == area && p.ControllerName == controller && p.ActionName == null) ||
                (p.Area == area && p.ControllerName == controller && p.ActionName == action)
            );
            if (obj)
            {
                await _next(httpContext);
                return;
            }
            else
            {
                _logger.LogWarning(EventIdList.Error,
"⚠️ کاربر {UserId} ({UserName}) دسترسی غیرمجاز -> Area={Area}, Controller={Controller}, Action={Action}, Path={Path}",
User.GetUserId(),
User.Identity.Name,
area ?? "-",
controller ?? "-",
action ?? "-",
httpContext.Request.Path
);
                httpContext.Response.Redirect("/AccessDenied");

                return;
            }




        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class PermissionCheckMiddlewareExtensions
{
    public static IApplicationBuilder UsePermissionCheckMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PermissionCheckMiddleware>();
    }
}

