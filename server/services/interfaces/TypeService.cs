using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Services.Answers;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class TypeService : ITypeService
   {
      private readonly ITypeStore _typeStore;

      public TypeService(ITypeStore typeStore)
      {
         _typeStore = typeStore;
      }

      public async Task<IList<NameAnswer>> GetAsync()
      {
         var types = await _typeStore.GetAsync();

         if (types == null) {
            return new List<NameAnswer>();
         }

         return types
         .Select(type => new NameAnswer{
            Id = type.Id,
            Name = type.Name
         }).ToList();
      }

      public async Task<IList<NameGroupAnswer>> GetGroupAsync()
      {
         var types = await _typeStore.GetAsync();

         if (types == null) {
            return new List<NameGroupAnswer>();
         }

         return types.GroupBy(
            type => type.Category,
            type => type,
            (category, options) => new NameGroupAnswer {
               Id = category.Id,
               Label = category.Name,
               Options = options.Select(option => new NameAnswer {
                  Id = option.Id,
                  Name = option.Name
               }).ToList()
            }
         ).OrderBy(item => item.Id).ToList();
      }
   }
}