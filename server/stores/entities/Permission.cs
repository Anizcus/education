using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Permission
   {
      #region properties
      public uint Id { get; set; }
      public string Name { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public IList<RolePermission> PermissionRoles { get; set; }
      #endregion
   }
}