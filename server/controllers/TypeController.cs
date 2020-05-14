using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Payloads;
using Server.Services.Interfaces;

namespace Server.Controllers
{
   [Authorize]
   [ApiController]
   public class TypeController : ControllerBase
   {
      private readonly ITypeService _typeService;
      public TypeController(ITypeService typeService) {
         _typeService = typeService;
      }

      [AllowAnonymous]
      [HttpGet("/type/all")]
      public async Task<IActionResult> Get() 
      {
         var types = await _typeService.GetGroupAsync();
         
         // var payload = types.Select(type => new NamePayload {
         //    Id = type.Id,
         //    Name = type.Name
         // }).ToList();

         return Ok(types);
      }

   }
}