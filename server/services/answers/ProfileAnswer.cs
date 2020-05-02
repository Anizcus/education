using System.Collections.Generic;

namespace Server.Services.Answers
{
   public class ProfileAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public uint Level { get; set; }
      public uint Experience { get; set; }
      public uint NextExperience { get; set; }
      public string Role { get; set; }
      public IList<LessonAnswer> Lessons{ get; set; }
   }
}