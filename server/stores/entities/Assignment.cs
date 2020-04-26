using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Assignment
   {
      #region properties
      public uint Id { get; set; }
      public string Description { get; set; }
      public uint Experience { get; set; }
      public string Answer { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      public uint LessonId { get; set; }
      #endregion
      #region navigation
      public Lesson Lesson { get; set; }
      public IList<UserAssignment> AssignmentUsers { get; set; }
      #endregion
   }
}