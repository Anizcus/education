using System.Collections.Generic;

namespace Server.Models.Payloads
{
   public class NameListPayload
   {
      public IList<NamePayload> Names { get; set; }
   }
}