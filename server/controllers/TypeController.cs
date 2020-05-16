using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Payloads;
using Server.Models.Requests;
using Server.Services.Interfaces;

namespace Server.Controllers
{
   [Authorize]
   [ApiController]
   public class TypeController : ControllerBase
   {
      private readonly IAdminService _adminService;
      public TypeController(IAdminService adminService) {
         _adminService = adminService;
      }

      [AllowAnonymous]
      [HttpGet("/type/all")]
      public async Task<IActionResult> Get() 
      {
         var types = await _adminService.GetGroupTypeBasedAsync();

         return Ok(types);
      }

      [HttpPost("/type")]
      public async Task<IActionResult> CreateType([FromBody] RequestTypeCreate request) 
      {
         var types = await _adminService.CreateTypeAsync(request.Id, request.Name);

         return Ok(types);
      }

   }
}