using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface IUserService
   {
      Task<NameAnswer> GetAsync(uint id);
      Task<NameAnswer> GetAsync(IEnumerable<Claim> claims);
      Task<NameAnswer> SignUpAsync(string username, string password);
      Task<SessionAnswer> SignInAsync(string username, string password);
      Task<SessionAnswer> RestoreAsync(string session, string account);
   }
}