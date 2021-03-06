﻿using System;
using System.Linq;
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
   public class UserController : ControllerBase
   {
      private readonly IUserService _userService;

      public UserController(IUserService userService)
      {
         _userService = userService;
      }

      [Permission((uint)PermissionEnum.User.View)]
      [HttpGet("/user")]
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
               Id = user.Id,
               Name = user.Name
            }
         );
      }
      
      [AllowAnonymous]
      [HttpGet("/users")]
      public async Task<IActionResult> Get()
      {
         var users = await _userService.GetAsync();

         var payload = users.Select(user => new UserListPayload{
            Id = user.Id,
            Name = user.Name,
            Role = user.Role,
            Level = user.Level
         }).ToList();

         return Ok(payload);
      }

      [AllowAnonymous]
      [HttpGet("/user/roles")]
      public async Task<IActionResult> GetRoles([FromQuery] RoleRequest request)
      {
         var roles = request.ForRegistration 
            ? await _userService.GetRolesForRegisterAsync()
            : await _userService.GetRolesAsync();

         var payload = roles.Select(role => new NamePayload{
            Id = role.Id,
            Name = role.Name,
         }).ToList();

         return Ok(payload);
      }

      [AllowAnonymous]
      [HttpPost("/user/signup")]
      public async Task<IActionResult> SignUp([FromBody] RequestSignUp request)
      {
         var user = await _userService.SignUpAsync(request.Username, request.Password, request.Role);

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
               Id = user.Id,
               Name = user.Name,
               Role = user.Role,
               Session = user.Session
            }
         );
      }

      [HttpPost("/user/online")]
      public async Task<IActionResult> Online()
      {
         var user = await _userService.GetAsync(HttpContext.User.Claims);

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
               Id = user.Id,
               Name = user.Name,
               Role = user.Role,
               Session = "You are logged in!"
            }
         );
      }

      [Permission((uint) PermissionEnum.User.Modify)]
      [HttpPost("/user/modify/status")]
      public async Task<IActionResult> ModifyUser([FromBody] ModifyUserRequest request)
      {
         var user = HttpContext.User.Claims.ElementAt(0);

         if (uint.Parse(user.Value) == request.UserId) {
            return BadRequest(
               new ErrorPayload
               {
                  Error = "You can't modify yourself!"
               }
            );
         }

         var status = await _userService
            .PostModifyUserStatusAsync(
               request.UserId, 
               request.RoleId, 
               request.IsBlocked
         );

         if (!String.IsNullOrEmpty(status.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = status.Error
               }
            );
         }

         return Ok(
            new NamePayload
            {
               Id = status.Id,
               Name = status.Name
            }
         );
      }

      [HttpGet("/user/profile")]
      public async Task<IActionResult> Profile([FromQuery] RequestById request)
      {
         var user = await _userService.GetProfileAsync(request.Id);

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
            new ProfilePayload
            {
               Id = user.Id,
               Name = user.Name,
               Level = user.Level,
               Experience = user.Experience,
               NextExperience = user.NextExperience,
               Role = user.Role,
               Lessons = user.Lessons.Select(lesson => new LessonPayload
               {
                  OwnerId = lesson.OwnerId,
                  OwnerName = lesson.OwnerName,
                  State = lesson.State,
                  Id = lesson.Id,
                  Name = lesson.Name,
                  Category = lesson.Category,
                  Description = lesson.Description,
                  Type = lesson.Type,
                  Status = lesson.Status,
                  Progress = lesson.Progress,
                  BadgeBase64 = $"data:image/png;base64,{System.Convert.ToBase64String(lesson.Badge)}"
               }).ToList()
            }
         );
      }
   }
}
