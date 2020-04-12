using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Progress
   {
      #region properties
      public uint Id { get; set; }
      public string Name { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public IList<UserLesson> UserLessons { get; set; }
      public IList<UserAssignment> UserAssignments { get; set; }
      #endregion
   }
}