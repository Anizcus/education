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

      public async Task<Type> CreateAsync(uint categoryId, string name) {
         var type = new Type {
            CategoryId = categoryId,
            Name = name
         };

         var created = await _store.Types.AddAsync(type);
         var saved = await _store.SaveChangesAsync();

         return created.Entity;
      }

      public async Task<Type> GetAsync(string name)
      {
         return await _store.Types
            .Include(type => type.Category)
            .FirstOrDefaultAsync(type => type.Name == name);
      }

      public async Task<Type> GetAsync(uint id)
      {
         return await _store.Types
            .Include(type => type.Category)
            .FirstOrDefaultAsync(type => type.Id == id);
      }

      public async Task<Type> UpdateAsync(Type type)
      {
         var updated = _store.Types.Update(type);
         var saved = await _store.SaveChangesAsync();

         return updated.Entity;
      }
   }
}