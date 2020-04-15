using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Lesson
   {
      #region properties
      public uint Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string Status { get; set; }
      public byte[] Badge { get; set; }
      public uint StateId { get; set; }
      public uint CategoryId { get; set; }
      public uint TypeId { get; set; }
      public uint OwnerId { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public State State { get; set; }
      public User Owner { get; set; }
      public Type Type { get; set; }
      public Category Category { get; set; }
      public IList<Assignment> Assignments { get; set; }
      public IList<UserLesson> LessonUsers { get; set; }
      #endregion
   }
}