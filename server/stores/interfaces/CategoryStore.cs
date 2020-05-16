using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces
{
   public class CategoryStore : ICategoryStore
   {
      private readonly Store _store;
      public CategoryStore(Store store)
      {
         _store = store;
      }

      public async Task<Category> CreateAsync(string name)
      {
         var category = new Category {
            Name = name
         };

         var created = await _store.Categories
            .AddAsync(category);
         var saved = await _store.SaveChangesAsync();

         return created.Entity;
      }

      public async Task<IList<Category>> GetAsync()
      {
         return await _store.Categories
            .Include(category => category.Types)
            .ToListAsync();
      }

      public async Task<Category> GetAsync(uint id)
      {
         return await _store.Categories
            .Include(category => category.Types)
            .FirstOrDefaultAsync(category => category.Id == id);
      }

      public async Task<Category> GetAsync(string name)
      {
         return await _store.Categories
            .Include(category => category.Types)
            .FirstOrDefaultAsync(category => category.Name == name);
      }
   }
}