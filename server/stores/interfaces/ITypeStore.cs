using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface ITypeStore
   {
      Task<IList<Type>> GetAsync();
      Task<Type> GetAsync(string name);
      Task<Type> GetAsync(uint id);
      Task<Type> CreateAsync(uint categoryId, string name);
      Task<Type> UpdateAsync(Type type);
   }
}