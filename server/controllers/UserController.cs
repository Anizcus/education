using System;
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
   public class UserController: ControllerBase
   {
      private readonly IUserService _userService;

      public UserController(IUserService userService)
      {
         _userService = userService;
      }

      [Permission((uint)PermissionEnum.User.View)]
      [HttpGet("/user/all")]
      public async Task<IActionResult> Get([FromBody] RequestById request)
      {
         var user = await _userService.GetAsync(request.Id);

         if (!String.IsNullOrEmpty(user.Error))
         {
            return BadRequest(
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

      [AllowAnonymous]
      [HttpPost("/user/signup")]
      public async Task<IActionResult> SignUp([FromBody] RequestSignUp request)
      {
         var user = await _userService.SignUpAsync(request.Username, request.Password);

         if (!String.IsNullOrEmpty(user.Error))
         {
            return BadRequest(
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

      [AllowAnonymous]
      [HttpPost("/user/signin")]
      public async Task<IActionResult> SignIn([FromBody] RequestSignIn request) 
      {
         var user = await _userService.SignInAsync(request.Username, request.Password);

         if (!String.IsNullOrEmpty(user.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = user.Error
               }
            );
         }

         return Ok(
            new SessionPayload
            {
               User = {
                  Id = user.Id,
                  Name = user.Name
               },
               Session = user.Session
            }
         );
      }
   }
}
