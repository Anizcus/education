namespace Server.Services.Answers
{
   public class AssignmentAnswerStatus: ErrorAnswer
   {
      public uint AssignmentId { get; set; }
      public string Message { get; set; }
      public string Progress { get; set; }
   }
}