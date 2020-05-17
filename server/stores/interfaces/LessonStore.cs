using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces
{
   public class LessonStore : ILessonStore
   {
      private readonly Store _store;
      public LessonStore(Store store)
      {
         _store = store;
      }

      public async Task<Lesson> CreateLessonAsync(uint typeId, uint ownerId, string name, string description, byte[] badge)
      {
         var type = await _store.Types.FirstOrDefaultAsync(type => type.Id == typeId);

         var entity = new Lesson
         {
            Name = name,
            Description = description,
            OwnerId = ownerId,
            CategoryId = type.CategoryId,
            TypeId = typeId,
            Badge = badge,
            Status = "",
            StateId = (uint) StateEnum.Created
         };

         var added = await _store.Lessons.AddAsync(entity);
         var saved = await _store.SaveChangesAsync();

         return added.Entity;
      }

      public async Task<Lesson> GetLessonDataAsync(uint id)
      {
         var lesson = await _store.Lessons
            .Include(lesson => lesson.Assignments)
               .ThenInclude(assignment => assignment.AssignmentUsers)
                  .ThenInclude(user => user.Progress)
            .Include(lesson => lesson.Category)
            .Include(lesson => lesson.Owner)
            .Include(lesson => lesson.State)
            .Include(lesson => lesson.Type)
            .Include(lesson => lesson.LessonUsers)
               .ThenInclude(user => user.Progress)
         .Where(lesson => lesson.Id == id)
         .FirstOrDefaultAsync();

         return lesson;
      }

      public async Task<Lesson> GetAsync(uint id)
      {
         return await _store.Lessons
            .Include(lesson => lesson.Assignments)
            .Include(lesson => lesson.Category)
            .Include(lesson => lesson.Owner)
            .Include(lesson => lesson.State)
            .Include(lesson => lesson.Type)
         .Where(lesson => lesson.Id == id)
         .FirstOrDefaultAsync();
      }

      public async Task<IList<Lesson>> GetByTypAllAsync(uint typeId)
      {
         return await _store.Lessons
            .Include(lesson => lesson.Owner)
            .Include(lesson => lesson.State)
            .Include(lesson => lesson.Type)
            .Include(lesson => lesson.Category)
         .Where(lesson => lesson.TypeId == typeId)
         .ToListAsync();
      }

      public async Task<IList<Lesson>> GetByTypePublishedAsync(uint typeId)
      {
         return await _store.Lessons
            .Include(Lesson => Lesson.Owner)
            .Include(lesson => lesson.State)
            .Include(lesson => lesson.Type)
            .Include(lesson => lesson.Category)
         .Where(lesson => lesson.TypeId == typeId && lesson.StateId == (uint)StateEnum.Published)
         .ToListAsync();
      }

      public async Task<IList<Category>> GetCategoriesAsync()
      {
         return await _store.Categories.ToListAsync();
      }

      public async Task<IList<Type>> GetTypesByCategoryAsync(uint categoryId)
      {
         return await _store.Types.Where(type => type.CategoryId == categoryId)
            .ToListAsync();
      }

      public async Task<Lesson> PublishLessonAssignmentsAsync(uint lessonId, IList<Assignment> assignments)
      {
         var entity = await _store.Lessons
            .Include(l => l.Assignments)
         .Where(l => l.Id == lessonId).FirstOrDefaultAsync();

         entity.StateId = (uint) StateEnum.Waiting;

         foreach(var assignment in assignments) {
            entity.Assignments.Add(assignment);
         }

         var updated = _store.Lessons.Update(entity);
         var saved = await _store.SaveChangesAsync();

         return updated.Entity;
      }

      public async Task<Lesson> UpdateLessonStatusAsync(Lesson lesson)
      {
         var updated = _store.Lessons.Update(lesson);
         var saved = await _store.SaveChangesAsync();

         return updated.Entity;
      }

      public async Task<Assignment> GetAssignment(uint id)
      {
         return await _store.Assignments
            .Include(assignment => assignment.AssignmentUsers)
               .ThenInclude(user => user.Progress)
            .Include(assignment => assignment.AssignmentUsers)
               .ThenInclude(user => user.User)
            .Where(assignment => assignment.Id == id)
            .FirstOrDefaultAsync();
      }

      public async Task<User> PostUserAsync(User user)
      {
         var updated = _store.Users.Update(user);
         var saved = await _store.SaveChangesAsync();

         return updated.Entity;
      }

      public async Task<IList<Assignment>> GetLessonAssignments(uint lessonId)
      {
         return await _store.Assignments
            .Where(assignment => assignment.LessonId == lessonId)
            .ToListAsync();
      }

      public async Task<UserLesson> GetUserLesson(uint lessonId, uint userId)
      {
         return await _store.UserLessons
            .Include(lesson => lesson.User)
            .Include(lesson => lesson.Lesson)
            .Include(lesson => lesson.Progress)
            .FirstOrDefaultAsync(lesson => lesson.LessonId == lessonId && lesson.UserId == userId);
      }

      public async Task<UserLesson> PostUserLesson(UserLesson lesson)
      {
         var updated = _store.UserLessons.Update(lesson);
         var saved = await _store.SaveChangesAsync();

         return updated.Entity;
      }

      public async Task<UserAssignment> PostUserAssignment(UserAssignment assignment)
      {
         var updated = _store.UserAssignments.Update(assignment);
         var saved = await _store.SaveChangesAsync();

         return updated.Entity;
      }

      public async Task<UserAssignment> GetUserAssignment(uint assignmentId, uint userId)
      {
         return await _store.UserAssignments
            .Include(assignment => assignment.User)
            .Include(assignment => assignment.Assignment)
            .Include(assignment => assignment.Progress)
            .FirstOrDefaultAsync(assignment => assignment.AssignmentId == assignmentId && assignment.UserId == userId);
      }

      public async Task<IList<UserAssignment>> GetUserAssignments(uint userId)
      {
         return await _store.UserAssignments
            .Include(assignment => assignment.User)
            .Include(assignment => assignment.Assignment)
            .Include(assignment => assignment.Progress)
            .Where(assignment => assignment.UserId == userId).ToListAsync();
      }
      public async Task<IList<UserAssignment>> GetUserAssignmentsBasedOnProgressAndLesson(uint userId, uint lessonId, uint progressId)
      {
         return await _store.UserAssignments
            .Include(assignment => assignment.User)
            .Include(assignment => assignment.Assignment)
            .Include(assignment => assignment.Progress)
            .Where(assignment => 
               assignment.UserId == userId && 
               assignment.Assignment.LessonId == lessonId && 
               assignment.ProgressId == progressId)
            .ToListAsync();
      }

      public async Task<IList<Lesson>> GetLessonListForAdmin()
      {
         return await _store.Lessons
            .Include(Lesson => Lesson.Owner)
            .Include(lesson => lesson.State)
            .Include(lesson => lesson.Type)
            .Include(lesson => lesson.Category)
         .Where(lesson => lesson.StateId == (uint) StateEnum.Waiting)
         .ToListAsync();
      }

      public async Task<IList<Lesson>> GetLessonListForOwner(uint userId)
      {
         return await _store.Lessons
            .Include(Lesson => Lesson.Owner)
            .Include(lesson => lesson.State)
            .Include(lesson => lesson.Type)
            .Include(lesson => lesson.Category)
         .Where(lesson => lesson.OwnerId == userId)
         .ToListAsync();
      }
   }
}