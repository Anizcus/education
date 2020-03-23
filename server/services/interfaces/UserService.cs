using System.Threading.Tasks;
using Server.Services.Answers;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class UserService : IUserService
   {
      private readonly IUserStore _userStore;

      public UserService(IUserStore userStore){
         _userStore = userStore;
      }
      public async Task<NameAnswer> GetAsync(int id)
      {
         var user = await _userStore.GetAsync(id);

         if (user == null) {
            return new NameAnswer {
               Error = "User not found!"
            };
         }

         return new NameAnswer {
            Name = user.Name
         };
      }

      public Task<SessionAnswer> LogInAsync(string username, string password)
      {
         throw new System.NotImplementedException();
      }

      public Task<NameAnswer> RegisterAsync(string username, string password)
      {
         throw new System.NotImplementedException();
      }

      public Task<SessionAnswer> RestoreAsync(string session, string account)
      {
         throw new System.NotImplementedException();
      }
   }
}