using System;

namespace Server.Stores.Entities
{
   public class RolePermission
   {
      #region properties
      public uint PermissionId { get; set; }
      public uint RoleId { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public Role Role { get; set; }
      public Permission Permission { get; set; }
      #endregion
   }
}