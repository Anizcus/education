using System.Collections.Generic;

namespace Server.Services.Answers
{
   public class NameGroupAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Label { get; set; }
      public IList<NameAnswer> Options { get; set; }
   }
}