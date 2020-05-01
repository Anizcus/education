namespace Server.Models.Requests
{
   public class AssignmentAnswerRequest
   {
      public uint AssignmentId { get; set; }
      public string Answer { get; set; }
   }
}