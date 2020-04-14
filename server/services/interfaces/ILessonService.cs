using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface ILessonService
   {
      Task<IList<NameAnswer>> GetCategoriesAsync();
      Task<IList<NameAnswer>> GetTypesByCategoryAsync(uint id);
      Task<IList<LessonListAnswer>> GetByTypePublishedAsync(uint id);
   }
}