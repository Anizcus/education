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

      public async Task<Lesson> CreateLessonAsync(uint typeId, uint ownerId, string name, string description, byte[] badge)
      {
         var type = await _store.Types.FirstOrDefaultAsync(type => type.Id == typeId);

         var entity = new Lesson
         {
            Name = name,
            Description = description,
            OwnerId = ownerId,
            CategoryId = type.CategoryId,
            TypeId = typeId,
            Badge = badge,
            Status = "",
            StateId = (uint) StateEnum.Created
         };

         var added = await _store.Lessons.AddAsync(entity);
         var saved = await _store.SaveChangesAsync();

         return added.Entity;
      }

      public async Task<IList<Lesson>> GetByTypePublishedAsync(uint typeId)
      {
         return await _store.Lessons
            .Include(Lesson => Lesson.Owner)
         .Where(lesson => lesson.TypeId == typeId && lesson.StateId == (uint)StateEnum.Published)
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