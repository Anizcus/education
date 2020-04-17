using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface ILessonStore
   {
      Task<IList<Category>> GetCategoriesAsync();
      Task<IList<Type>> GetTypesByCategoryAsync(uint categoryId);
      Task<IList<Lesson>> GetByTypePublishedAsync(uint typeId);
      Task<IList<Lesson>> GetByTypAllAsync(uint typeId);
      Task<Lesson> CreateLessonAsync(uint typeId, uint ownerId, string name, string description, byte[] badge);
   }
}