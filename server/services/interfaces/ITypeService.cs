using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Services.Answers;

namespace Server.Services.Interfaces
{
   public interface ITypeService
   {
      Task<IList<NameAnswer>> GetAsync();
      Task<IList<NameGroupAnswer>> GetGroupAsync();
   }
}