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

      public async Task<IList<NameAnswer>> GetCategoriesAsync()
      {
         var categories = await _lessonStore.GetCategoriesAsync();

         return categories.Select(
            category => new NameAnswer {
               Id = category.Id,
               Name = category.Name
         }).ToList();
      }
   }
}