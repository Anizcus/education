namespace Server.Models.Requests
{
   public class ModifyUserRequest
   {
      public uint UserId { get; set; }
      public uint RoleId { get; set; }
      public bool IsBlocked { get; set; }
   }
}
