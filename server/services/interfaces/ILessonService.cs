using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface ILessonService
   {
      Task<IList<NameAnswer>> GetCategoriesAsync();
      Task<IList<NameAnswer>> GetTypesByCategoryAsync(uint categoryId);
      Task<IList<LessonListAnswer>> GetByTypePublishedAsync(uint typeId);
      Task<IList<LessonListAnswer>> GetByTypeAllAsync(uint typeId);
      Task<NameAnswer> CreateLessonAsync(uint typeId, uint ownerId, string name, string description, byte[] badge);
   }
}