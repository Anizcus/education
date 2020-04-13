using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces
{
   public class LessonStore : ILessonStore
   {
      private readonly Store _store;
      public LessonStore(Store store)
      {
         _store = store;
      }

      public async Task<IList<Category>> GetCategoriesAsync()
      {
         return await _store.Categories.ToListAsync();
      }
   }
}