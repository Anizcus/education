namespace Server.Models.Payloads
{
   public class SessionPayload
   {
      public NamePayload User { get; set; }
      public string Session { get; set; }
   }
}
