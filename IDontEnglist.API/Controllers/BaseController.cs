using IDonEnglist.Application.Constants;
using IDonEnglist.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace IDonEnglist.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected CurrentUser GetUserFromToken()
        {
            var userIdClaim = User.FindFirst(CustomClaimTypes.Id);
            var userNameClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);

            var currentUser = new CurrentUser
            {
                Id = int.Parse(userIdClaim?.Value ?? "0"),
                Name = userNameClaim?.Value ?? ""
            };

            return currentUser;
        }
    }
}
