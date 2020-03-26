namespace Server.Services.Answers
{
   public class SessionAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public string Session { get; set; }
   }
}