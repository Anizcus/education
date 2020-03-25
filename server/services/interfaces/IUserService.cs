using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface IUserService
   {
      Task<NameAnswer> GetAsync(int id);
      Task<NameAnswer> SignUpAsync(string username, string password);
      Task<SessionAnswer> SignInAsync(string username, string password);
      Task<SessionAnswer> RestoreAsync(string session, string account);
   }
}