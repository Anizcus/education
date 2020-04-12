using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Category
   {
      #region properties
      public uint Id { get; set; }
      public string Name { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public IList<Lesson> Lessons { get; set; }
      public IList<Type> Types { get; set; }
      #endregion
   }
}