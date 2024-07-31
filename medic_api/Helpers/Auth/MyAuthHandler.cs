using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace medic_api.Helpers.Auth
{
    public class MyAuthHandler : TypeFilterAttribute
    {
        public string Roles { get; }

        public MyAuthHandler(string roles ="") : base(typeof(MyAuthAsyncActionFilter))
        {
            Roles = roles;
            Arguments = new object[] { roles };
        }
    }
    public class MyAuthAsyncActionFilter : IAsyncActionFilter
    {
        private readonly string _role;

        public MyAuthAsyncActionFilter(string role)
        {
            _role = role;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetService<MyAuthService>()!;
            if (!authService.IsLogiran())
            {
                context.Result = new UnauthorizedObjectResult("Niste logirani");
                return;
            }

            var role = _role.ToLower();

            var niz = role.Split(',');

            var isAdmin = await authService.IsAdmin();
            if (niz.Contains("admin") && !isAdmin)
            {
                context.Result = new UnauthorizedObjectResult("Nemate privilegije");
                return;
            }

            MyAuthInfo myAuthInfo = authService.GetAuthInfo();

            await next();
        }
    }
}
