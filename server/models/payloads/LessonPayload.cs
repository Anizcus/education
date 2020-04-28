using System.Collections.Generic;

namespace Server.Models.Payloads
{
   public class LessonPayload
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public uint OwnerId { get; set; }
      public string OwnerName { get; set; }
      public string Progress { get; set; }
      public string State { get; set; }
      public string BadgeBase64 { get; set; }
      public string Status { get; set; }
      public string Category { get; set; }
      public string Type { get; set; }
      public IList<AssignmentPayload> Assignments { get; set; } 
   }
}
