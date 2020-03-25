using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface IUserStore
   {
      Task<User> GetAsync(int id);
      Task<User> GetAsync(string name);
      Task<IList<User>> GetAsync();
      Task<User> CreateAsync(User entity);
   }
}