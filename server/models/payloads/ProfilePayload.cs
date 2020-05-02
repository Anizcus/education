using System.Collections.Generic;

namespace Server.Models.Payloads
{
   public class ProfilePayload
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public uint Level { get; set; }
      public uint Experience { get; set; }
      public uint NextExperience { get; set; }
      public string Role { get; set; }
      public IList<LessonPayload> Lessons{ get; set; }
   }
}