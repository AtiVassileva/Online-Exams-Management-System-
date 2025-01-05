using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineExamSystem.API.Infrastructure;

namespace OnlineExamSystem.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireAuthorization : Attribute, IAuthorizationFilter
    {
        private readonly string[]? _roles;

        public RequireAuthorization()
        {
        }

        public RequireAuthorization(string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.GetAuthToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_roles?.Length > 0)
            {
                var userRole = context.HttpContext.GetUserRole();

                if (string.IsNullOrWhiteSpace(userRole) || !_roles.Contains(userRole))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}