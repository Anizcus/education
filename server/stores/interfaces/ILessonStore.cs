using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces {
   public interface ILessonStore
   {
      Task<IList<Category>> GetCategoriesAsync();
      Task<IList<Type>> GetTypesByCategoryAsync(uint categoryId);
      Task<IList<Lesson>> GetByTypePublishedAsync(uint typeId);
      Task<IList<Lesson>> GetByTypAllAsync(uint typeId);
      Task<Lesson> CreateLessonAsync(uint typeId, uint ownerId, string name, string description, byte[] badge);
      Task<Lesson> UpdateLessonStatusAsync(Lesson lesson);
      Task<Lesson> PublishLessonAssignmentsAsync(uint lessonId, IList<Assignment> assignments);
      Task<Lesson> GetLessonDataAsync(uint id);
      Task<Lesson> GetAsync(uint id);
      Task<Assignment> GetAssignment(uint id);
      Task<IList<Assignment>> GetLessonAssignments(uint lessonId);
      Task<User> PostUserAsync(User user);
      Task<UserLesson> GetUserLesson(uint lessonId, uint userId);
      Task<UserLesson> PostUserLesson(UserLesson lesson);
      Task<UserAssignment> GetUserAssignment(uint assignmentId, uint userId);
      Task<IList<UserAssignment>> GetUserAssignments(uint userId);
      Task<UserAssignment> PostUserAssignment(UserAssignment assignment);
      Task<IList<UserAssignment>> GetUserAssignmentsBasedOnProgressAndLesson(uint userId, uint lessonId, uint progressId);
      Task<IList<Lesson>> GetLessonListForAdmin();
      Task<IList<Lesson>> GetLessonListForOwner(uint userId);
      
   }
}