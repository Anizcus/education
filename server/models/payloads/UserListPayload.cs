namespace Server.Models.Payloads
{
   public class UserListPayload
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public uint Level { get; set; }
      public string Role { get; set; }
   }
}