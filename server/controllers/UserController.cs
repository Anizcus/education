using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Attributes;
using Server.Enums;
using Server.Models.Payloads;
using Server.Models.Requests;
using Server.Services.Interfaces;

namespace Server.Controllers
{
   [Authorize]
   [ApiController]
   [Route("[controller]")]
   public class UserController : ControllerBase
   {
      private readonly IUserService _userService;

      public UserController(IUserService userService)
      {
         _userService = userService;
      }

      [HttpGet]
      [Permission((uint) PermissionEnum.User.View)]
      public async Task<ActionResult> Get([FromQuery] RequestById request)
      {
         var user = await _userService.GetAsync(request.Id);
         if (user.Error != null)
         {
            return NotFound(
               new ErrorPayload
               {
                  Error = user.Error
               }
            );
         }

         return Ok(
            new NamePayload
            {
               Name = user.Name
            }
         );
      }
   }
}
