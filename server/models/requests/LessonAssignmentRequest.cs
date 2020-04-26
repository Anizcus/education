using System.Collections.Generic;

namespace Server.Models.Requests
{
   public class LessonAssignmentRequest
   {
      public uint Id { get; set; }
      public string Type { get; set; }
      public IList<AssignmentRequest> Assignments { get; set; } 
   }
}
