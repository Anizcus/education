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
   }
}