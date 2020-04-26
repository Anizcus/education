using System.Collections.Generic;

namespace Server.Services.Answers
{
   public class LessonAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public uint OwnerId { get; set; }
      public string OwnerName { get; set; }
      public string Type { get; set; }
      public string State { get; set; }
      public byte[] Badge { get; set; }
      public string Status { get; set; }
      public string Category { get; set; }
      public IList<AssignmentAnswer> Assignments { get; set; }
   }
}