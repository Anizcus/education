using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces
{
   public class TypeStore : ITypeStore
   {
      private readonly Store _store;
      public TypeStore(Store store)
      {
         _store = store;
      }

      public async Task<IList<Type>> GetAsync()
      {
         return await _store.Types
            .Include(type => type.Category)
            .ToListAsync();
      }
   }
}