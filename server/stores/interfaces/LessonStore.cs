using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Enums;
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

      public async Task<IList<Lesson>> GetByTypePublishedAsync(uint typeId)
      {
         return await _store.Lessons
            .Include(Lesson => Lesson.Owner)
            .Include(Lesson => Lesson.Status)
         .Where(lesson => lesson.TypeId == typeId && lesson.Status.StateId == (uint)StateEnum.Published)
         .ToListAsync();
      }

      public async Task<IList<Category>> GetCategoriesAsync()
      {
         return await _store.Categories.ToListAsync();
      }

      public async Task<IList<Type>> GetTypesByCategoryAsync(uint categoryId)
      {
         return await _store.Types.Where(type => type.CategoryId == categoryId)
            .ToListAsync();
      }
   }
}