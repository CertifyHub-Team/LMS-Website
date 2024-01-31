using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
    public class CheckClaimsAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimKey;
        private readonly string _claimValue;

        public CheckClaimsAttribute(string claimKey, string claimValue)
        {
            _claimKey = claimKey;
            _claimValue = claimValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(_claimKey, _claimValue))
            {
                context.Result = new UnauthorizedResult();

            }
        }
    }
}
