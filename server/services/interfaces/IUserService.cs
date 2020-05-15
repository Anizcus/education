using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface IUserService
   {
      Task<NameAnswer> GetAsync(uint id);
      Task<SessionAnswer> GetAsync(IEnumerable<Claim> claims);
      Task<NameAnswer> SignUpAsync(string username, string password, uint role);
      Task<SessionAnswer> SignInAsync(string username, string password);
      Task<SessionAnswer> RestoreAsync(string session, string account);
      Task<ProfileAnswer> GetProfileAsync(uint userId);
      Task<IList<UserListAnswer>> GetAsync();
      Task<IList<NameAnswer>> GetRolesAsync();
      Task<NameAnswer> PostModifyUserStatusAsync(uint userId, uint role, bool isBlocked);
      Task<IList<NameAnswer>> GetRolesForRegisterAsync();
   }
}