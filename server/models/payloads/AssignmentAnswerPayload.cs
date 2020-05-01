namespace Server.Models.Payloads
{
   public class AssignmentAnswerPayload
   {
      public uint AssignmentId { get; set; }
      public string Message { get; set; }
      public string Progress { get; set; }
   }
}