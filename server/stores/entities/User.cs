using System;

namespace Server.Stores.Entities
{
   public class User
   {
      #region properties
      public uint Id { get; set; }
      public string Name { get; set; }
      public byte[] Password { get; set; }
      public byte[] Salt { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      public uint RoleId { get; set; }
      #endregion
      #region navigation
      public Role Role { get; set; }
      #endregion
   }
}