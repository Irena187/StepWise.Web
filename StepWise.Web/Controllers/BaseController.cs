using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace StepWise.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected bool IsUserAuthenticated()
        {
            return User?.Identity?.IsAuthenticated ?? false;
        }

        protected Guid GetUserId()
        {
            var userIdClaim = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            return Guid.Empty;
        }
    }
}
