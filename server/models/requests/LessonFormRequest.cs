using Microsoft.AspNetCore.Http;

namespace Server.Models.Requests
{
   public class LessonFormRequest
   {
      public string Name { get; set; }
      public string Description { get; set; }
      public IFormFile Badge { get; set; }
      public string Type { get; set; }
   }
}
