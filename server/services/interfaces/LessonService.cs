using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Services.Answers;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class LessonService : ILessonService
   {
      private readonly ILessonStore _lessonStore;

      public LessonService(ILessonStore lessonStore)
      {
         _lessonStore = lessonStore;
      }

      public async Task<IList<LessonListAnswer>> GetByTypePublishedAsync(uint typeId)
      {
         var lessons = await _lessonStore.GetByTypePublishedAsync(typeId);

         return lessons.Select(
            lesson => new LessonListAnswer {
               Id = lesson.Id,
               Name = lesson.Name,
               OwnerId = lesson.Owner.Id,
               OwnerName = lesson.Owner.Name
         }).ToList();
      }

      public async Task<IList<NameAnswer>> GetCategoriesAsync()
      {
         var categories = await _lessonStore.GetCategoriesAsync();

         return categories.Select(
            category => new NameAnswer {
               Id = category.Id,
               Name = category.Name
         }).ToList();
      }

      public async Task<IList<NameAnswer>> GetTypesByCategoryAsync(uint id)
      {
         var types = await _lessonStore.GetTypesByCategoryAsync(id);

         return types.Select(
            type => new NameAnswer {
               Id = type.Id,
               Name = type.Name
         }).ToList();
      }
   }
}