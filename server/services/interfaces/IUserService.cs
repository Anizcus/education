using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface IUserService
   {
      Task<NameAnswer> GetAsync(int id);
   }
}