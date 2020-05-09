using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
   public class LessonController : ControllerBase
   {
      private readonly ILessonService _lessonService;

      public LessonController(ILessonService lessonService)
      {
         _lessonService = lessonService;
      }

      [AllowAnonymous]
      [HttpGet("/lesson/categories")]
      public async Task<IActionResult> GetCategories()
      {
         var categories = await _lessonService.GetCategoriesAsync();

         var payload = new NameListPayload
         {
            Names = categories.Select(category => new NamePayload
            {
               Id = category.Id,
               Name = category.Name
            }).ToList()
         };

         return Ok(payload);
      }

      [AllowAnonymous]
      [HttpGet("/lesson/types")]
      public async Task<IActionResult> GetTypesByCategory([FromQuery] RequestById request)
      {
         var types = await _lessonService.GetTypesByCategoryAsync(request.Id);

         var payload = new NameListPayload
         {
            Names = types.Select(type => new NamePayload
            {
               Id = type.Id,
               Name = type.Name
            }).ToList()
         };

         return Ok(payload);
      }

      [HttpGet("/lesson/published")]
      public async Task<IActionResult> GetByTypePublished([FromQuery] RequestById request)
      {
         var lessons = await _lessonService.GetByTypePublishedAsync(request.Id);

         var payload = lessons.Select(
           lesson => new LessonListPayload
           {
              Id = lesson.Id,
              Name = lesson.Name,
              OwnerId = lesson.OwnerId,
              OwnerName = lesson.OwnerName
           }).ToList();

         return Ok(payload);
      }

      [HttpGet("/lesson/all")]
      public async Task<IActionResult> GetByTypeAll([FromQuery] RequestById request)
      {
         var lessons = await _lessonService.GetByTypeAllAsync(request.Id);

         var payload = lessons.Select(
           lesson => new LessonListPayload
           {
              Id = lesson.Id,
              Name = lesson.Name,
              OwnerId = lesson.OwnerId,
              OwnerName = lesson.OwnerName,
              State = lesson.State,
              BadgeBase64 = $"data:image/png;base64,{System.Convert.ToBase64String(lesson.Badge)}"
           }).ToList();

         return Ok(payload);
      }

      [HttpPost("/lesson")]
      public async Task<IActionResult> PostLesson([FromForm] LessonFormRequest request)
      {
         var user = HttpContext.User.Claims.ElementAt(0);
         var memoryStream = new System.IO.MemoryStream();

         await request.Badge.CopyToAsync(memoryStream);

         var lesson = await _lessonService.CreateLessonAsync(
            uint.Parse(request.Type),
            uint.Parse(user.Value),
            request.Name,
            request.Description,
            memoryStream.ToArray()
         );

         var payload = new NamePayload
         {
            Id = lesson.Id,
            Name = lesson.Name
         };

         return Ok(payload);
      }

      [HttpGet("/lesson")]
      public async Task<IActionResult> GetLesson([FromQuery] RequestById request)
      {
         var user = HttpContext.User.Claims.ElementAt(0);
         var lesson = await _lessonService.GetAsync(request.Id, uint.Parse(user.Value));

         if (!String.IsNullOrEmpty(lesson.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = lesson.Error
               }
            );
         }

         var payload = new LessonPayload
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
            Assignments = lesson.Assignments.Select(assignment => new AssignmentPayload
            {
               Id = assignment.Id,
               Description = assignment.Description,
               Experience = assignment.Experience,
               Progress = assignment.Progress,
            }).ToList(),
            BadgeBase64 = $"data:image/png;base64,{System.Convert.ToBase64String(lesson.Badge)}"
         };

         return Ok(payload);
      }

      [HttpPost("/lesson/assignment")]
      public async Task<IActionResult> PostLessonAssignment([FromBody] LessonAssignmentRequest request)
      {
         var user = HttpContext.User.Claims.ElementAt(0);
         var lesson = await _lessonService.PostLessonAssignmentAsync(
            request.Id, 
            uint.Parse(user.Value), 
            request.Type, 
            request.Assignments
         );

         if (!String.IsNullOrEmpty(lesson.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = lesson.Error
               }
            );
         }

         var payload = new LessonPayload
         {
         
         };

         return Ok(payload);
      }

      [Permission((uint)PermissionEnum.Lesson.Authorize)]
      [HttpPost("/lesson/status")]
      public async Task<IActionResult> PostLessonStatus([FromBody] StatusRequest request)
      {
         var lesson = await _lessonService.PostLessonStatusAsync(
            request.LessonId, 
            request.IsValid, 
            request.Status
         );

         if (!String.IsNullOrEmpty(lesson.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = lesson.Error
               }
            );
         }

         var payload = new NamePayload
         {
            Id = lesson.Id,
            Name = lesson.Name
         };

         return Ok(payload);
      }

      [HttpPost("/lesson/start")]
      public async Task<IActionResult> StartLesson([FromBody] RequestById request)
      {
         var user = HttpContext.User.Claims.ElementAt(0);
         var lesson = await _lessonService.StartLessonAsync(
            request.Id, uint.Parse(user.Value)
         );

         if (!String.IsNullOrEmpty(lesson.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = lesson.Error
               }
            );
         }

         var payload = new NamePayload
         {
            Id = lesson.Id,
            Name = lesson.Name
         };

         return Ok(payload);
      }

      [HttpPost("/lesson/assignment/answer")]
      public async Task<IActionResult> AssignmentAnswer([FromBody] AssignmentAnswerRequest request)
      {
         var user = HttpContext.User.Claims.ElementAt(0);
         var assignment = await _lessonService.AssignmentAnswerAsync(
            request.AssignmentId, uint.Parse(user.Value), request.Answer
         );

         if (!String.IsNullOrEmpty(assignment.Error))
         {
            return BadRequest(
               new ErrorPayload
               {
                  Error = assignment.Error
               }
            );
         }

         var payload = new AssignmentAnswerPayload
         {
            AssignmentId = assignment.AssignmentId,
            Message = assignment.Message,
            Progress = assignment.Progress
         };

         return Ok(payload);
      }
   }
}
