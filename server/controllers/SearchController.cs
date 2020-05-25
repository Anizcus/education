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
   public class SearchController : ControllerBase
   {
      private readonly ILessonService _lessonService;
      public SearchController(ILessonService lessonService)
      {
         _lessonService = lessonService;
      }

      [AllowAnonymous]
      [HttpPost("/search")]
      public async Task<IActionResult> SearchLesson([FromBody] SearchRequest request)
      {
         var lessons = await _lessonService.GetSearchedLesson(
            request.TypeId,
            request.Name,
            HttpContext.User.Claims.Any()
               ? (uint.Parse(HttpContext.User.Claims.ElementAt(0).Value))
               : 0);

         var payload = lessons.Select(
           lesson => new LessonListPayload
           {
              Id = lesson.Id,
              Name = lesson.Name,
              OwnerId = lesson.OwnerId,
              OwnerName = lesson.OwnerName,
              State = lesson.State,
              BadgeBase64 = $"data:image/png;base64,{System.Convert.ToBase64String(lesson.Badge)}",
              Status = lesson.Status,
              Modified = lesson.Modified
           }).ToList();

         return Ok(payload);
      }
   }
}