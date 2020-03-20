using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces
{
   public class UserStore : IUserStore
   {
      private readonly Store _store;
      public UserStore(Store store)
      {
         _store = store;
      }
      public async Task<User> GetAsync(int id)
      {
         return await _store.Users
            .FirstOrDefaultAsync(user => user.Id == id);
      }

      public async Task<IList<User>> GetAsync()
      {
         return await _store.Users
            .ToListAsync();
      }
   }
}