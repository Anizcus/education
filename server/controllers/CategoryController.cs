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
   public class CategoryController : ControllerBase
   {
      private readonly IAdminService _adminService;
      public CategoryController(IAdminService adminService) {
         _adminService = adminService;
      }

      [AllowAnonymous]
      [HttpGet("/category/all")]
      public async Task<IActionResult> Get() 
      {
         var categories = await _adminService.GetGroupCategoryBasedAsync();

         return Ok(categories);
      }

      [HttpPost("/category")]
      public async Task<IActionResult> CreateType([FromBody] RequestNameCreate request) 
      {
         var category = await _adminService.CreateCategoryAsync(request.Name);

         return Ok(category);
      }

   }
}