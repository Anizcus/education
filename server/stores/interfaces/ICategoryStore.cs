using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface ICategoryStore
   {
      Task<IList<Category>> GetAsync();
      Task<Category> GetAsync(uint id);
      Task<Category> GetAsync(string name);
      Task<Category> CreateAsync(string name);
   }
}