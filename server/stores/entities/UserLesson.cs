using System;

namespace Server.Stores.Entities
{
   public class UserLesson
   {
      #region properties
      public uint UserId { get; set; }
      public uint LessonId { get; set; }
      public uint ProgressId { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public User User { get; set; }
      public Lesson Lesson { get; set; }
      public Progress Progress { get; set; }
      #endregion
   }
}