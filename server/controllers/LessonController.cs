using System;
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
   public class LessonController : ControllerBase
   {
      private readonly ILessonService _lessonService;

      public LessonController(ILessonService lessonService) {
         _lessonService = lessonService;
      }
      
      [HttpGet("/lesson/categories")]
      public async Task<IActionResult> GetCategories()
      {
         var categories = await _lessonService.GetCategoriesAsync();

         var payload = new NameListPayload {
            Names = categories.Select(category => new NamePayload {
               Id = category.Id,
               Name = category.Name
            }).ToList()
         };

         return Ok(payload);
      }

      // [HttpGet("/lesson/types")]
      // public async Task<IActionResult> GetTypesByCategory([FromBody] RequestById request)
      // {
      //    return Ok();
      // }
   }
}
