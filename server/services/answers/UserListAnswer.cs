namespace Server.Services.Answers
{
   public class UserListAnswer: ErrorAnswer
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public string Role { get; set; }
      public uint Level { get; set; }
   }
}