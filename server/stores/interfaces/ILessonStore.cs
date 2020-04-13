using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface ILessonStore
   {
      Task<IList<Category>> GetCategoriesAsync();
   }
}