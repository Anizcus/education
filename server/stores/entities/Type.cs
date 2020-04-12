using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Type
   {
      #region properties
      public uint Id { get; set; }
      public string Name { get; set; }
      public uint CategoryId { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public Category Category { get; set; }
      public IList<Lesson> Lessons { get; set; }
      #endregion
   }
}