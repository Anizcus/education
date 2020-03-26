using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface IUserStore
   {
      Task<User> GetAsync(uint id);
      Task<User> GetAsync(string name);
      Task<IList<User>> GetAsync();
      Task<User> CreateAsync(User entity);
      Task<IList<Permission>> GetPermissionsAsync(uint id);
   }
}