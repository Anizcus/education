using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface IAdminService
   {
      Task<IList<NameAnswer>> GetTypesAsync();
      Task<IList<NameAnswer>> GetCategoriesAsync();
      Task<IList<NameGroupAnswer>> GetGroupAsync();
      Task<NameGroupAnswer> CreateTypeAsync(uint categoryId, string typeName);
      Task<NameAnswer> CreateCategoryAsync(string name);
   }
}