using System;
using System.Collections.Generic;

namespace Server.Stores.Entities
{
   public class Status
   {
      #region properties
      public uint Id { get; set; }
      public uint StateId { get; set; }
      public string Description { get; set; }
      public DateTimeOffset Created { get; set; }
      public DateTimeOffset? Updated { get; set; }
      #endregion
      #region navigation
      public IList<Lesson> Lessons { get; set; }
      public State State { get; set; }
      #endregion
   }
}