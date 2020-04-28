using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class User
   {
      #region properties
      public uint Id { get; set; }
      public uint Level { get; set; }
      public uint Experience { get; set; }
      public string Name { get; set; }
      public byte[] Password { get; set; }
      public byte[] Salt { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      public uint RoleId { get; set; }
      #endregion
      #region navigation
      public Role Role { get; set; }
      public IList<Lesson> Lessons { get; set; }
      public IList<UserLesson> UserLessons { get; set; }
      public IList<UserAssignment> UserAssignments { get; set; }
      #endregion
   }
}