namespace Server.Services.Answers
{
   public class LessonListAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public uint OwnerId { get; set; }
      public string OwnerName { get; set; }
      public string State { get; set; }
      public byte[] Badge { get; set; }
   }
}