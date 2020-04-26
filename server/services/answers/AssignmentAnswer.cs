namespace Server.Services.Answers
{
   public class AssignmentAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Description { get; set; }
      public uint Experience { get; set; }
      public string Answer { get; set; }
   }
}