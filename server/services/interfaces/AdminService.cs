using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Services.Answers;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class AdminService : IAdminService
   {
      private readonly ITypeStore _typeStore;
      private readonly ICategoryStore _categoryStore;

      public AdminService(ITypeStore typeStore, ICategoryStore categoryStore)
      {
         _typeStore = typeStore;
         _categoryStore = categoryStore;
      }

      public async Task<NameGroupAnswer> CreateTypeAsync(uint categoryId, string typeName)
      {
         var answer = default(NameGroupAnswer);
         var category = await _categoryStore
            .GetAsync(categoryId);

         if (category == null)
         {
            answer.Error = "Category does not exist!";

            return answer;
         }

         var type = await _typeStore.GetAsync(typeName);

         if (type != null) {
            answer.Error = "Type name already exists!";

            return answer;
         }

         type = await _typeStore.CreateAsync(categoryId, typeName);

         if (type == null)
         {
            answer.Error = "Failed to create a type!";

            return answer;
         }

         var option = default(NameAnswer);
         
         answer.Id = category.Id;
         answer.Label = category.Name;
         option.Id = type.Id;
         option.Name = type.Name;
         answer.Options.Add(option);

         return answer;
      }

      public async Task<IList<NameAnswer>> GetTypesAsync()
      {
         var types = await _typeStore.GetAsync();

         if (types == null)
         {
            return new List<NameAnswer>();
         }

         return types
         .Select(type => new NameAnswer
         {
            Id = type.Id,
            Name = type.Name
         }).ToList();
      }

      public async Task<IList<NameGroupAnswer>> GetGroupAsync()
      {
         var types = await _typeStore.GetAsync();

         if (types == null)
         {
            return new List<NameGroupAnswer>();
         }

         return types.GroupBy(
            type => type.Category,
            type => type,
            (category, options) => new NameGroupAnswer
            {
               Id = category.Id,
               Label = category.Name,
               Options = options.Select(option => new NameAnswer
               {
                  Id = option.Id,
                  Name = option.Name
               }).ToList()
            }
         ).OrderBy(item => item.Id).ToList();
      }

      public async Task<IList<NameAnswer>> GetCategoriesAsync()
      {
         var categories = await _categoryStore.GetAsync();

         if (categories == null)
         {
            return new List<NameAnswer>();
         }

         return categories
         .Select(category => new NameAnswer
         {
            Id = category.Id,
            Name = category.Name
         }).ToList();
      }

      public async Task<NameAnswer> CreateCategoryAsync(string name)
      {
         var answer = default(NameAnswer);
         var category = await _categoryStore
            .GetAsync(name);

         if (category != null) {
            answer.Error = "Category does not exist!";

            return answer;
         }

         category = await _categoryStore.CreateAsync(name);

         if (category == null)
         {
            answer.Error = "Failed to create a category!";

            return answer;
         }

         answer.Id = category.Id;
         answer.Name = category.Name;

         return answer;
      }
   }
}