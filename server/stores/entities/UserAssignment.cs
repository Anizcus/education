using System;

namespace Server.Stores.Entities
{
   public class UserAssignment
   {
      #region properties
      public uint UserId { get; set; }
      public uint AssignmentId { get; set; }
      public uint ProgressId { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public User User { get; set; }
      public Assignment Assignment { get; set; }
      public Progress Progress { get; set; }
      #endregion
   }
}