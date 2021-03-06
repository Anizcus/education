using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Enums;
using Server.Models.Requests;
using Server.Services.Answers;
using Server.Stores.Entities;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class LessonService : ILessonService
   {
      private readonly ILessonStore _lessonStore;
      private readonly IUserStore _userStore;

      public LessonService(ILessonStore lessonStore, IUserStore userStore)
      {
         _lessonStore = lessonStore;
         _userStore = userStore;
      }

      public async Task<AssignmentAnswerStatus> AssignmentAnswerAsync(uint assignmentId, uint userId, string answer)
      {
         var userTask = await _lessonStore.GetUserAssignment(assignmentId, userId);

         if (userTask == null)
         {
            return new AssignmentAnswerStatus
            {
               Error = "Assignment is not found"
            };
         }

         if (userTask.ProgressId == (uint)ProgressEnum.Completed)
         {
            return new AssignmentAnswerStatus
            {
               Error = "Assignment is already completed!"
            };
         }

         if (userTask.Assignment.Answer != answer)
         {
            return new AssignmentAnswerStatus
            {
               Error = "Assignment answer does not match!"
            };
         }

         var user = userTask.User;
         var level = user.Level;
         var experience = user.Experience + userTask.Assignment.Experience;

         while (experience >= level * 15)
         {
            experience -= level * 15;
            level++;
         }

         var difference = level - user.Level;
         var message = difference > 0
            ? $"Correct! You have gained {difference} level(-s)!"
            : $"Correct! You have gained {userTask.Assignment.Experience} experience!";

         user.Level = level;
         user.Experience = experience;

         user = await _lessonStore.PostUserAsync(user);

         userTask.ProgressId = (uint)ProgressEnum.Completed;

         userTask = await _lessonStore.PostUserAssignment(userTask);

         var lessonAssignments = await _lessonStore
            .GetLessonAssignments(userTask.Assignment.LessonId);

         var countCompleted = await _lessonStore
            .GetUserAssignmentsBasedOnProgressAndLesson(
               userId,
               userTask.Assignment.LessonId,
               (uint)ProgressEnum.Completed);
         var countTotal = lessonAssignments.Count();

         if (countCompleted.Count() == countTotal)
         {
            var lesson = await _lessonStore.GetUserLesson(userTask.Assignment.LessonId, userId);

            lesson.ProgressId = (uint)ProgressEnum.Completed;
            lesson = await _lessonStore.PostUserLesson(lesson);
         }

         return new AssignmentAnswerStatus
         {
            AssignmentId = assignmentId,
            Message = message,
            Progress = nameof(ProgressEnum.Completed)
         };
      }

      public async Task<NameAnswer> CreateLessonAsync(uint typeId, uint ownerId, string name, string description, byte[] badge)
      {
         var lesson = await _lessonStore.CreateLessonAsync(typeId, ownerId, name, description, badge);

         if (lesson == null)
         {
            return new NameAnswer
            {
               Error = "Failed to create a lesson"
            };
         }

         return new NameAnswer
         {
            Name = lesson.Name,
            Id = lesson.Id
         };
      }

      public async Task<LessonAnswer> GetAsync(uint id, uint userId)
      {
         var lesson = await _lessonStore.GetLessonDataAsync(id);

         if (lesson == null)
         {
            return new LessonAnswer
            {
               Error = "Lesson does not exist!"
            };
         }

         return new LessonAnswer
         {
            OwnerId = lesson.Owner.Id,
            OwnerName = lesson.Owner.Name,
            State = lesson.State.Name,
            Id = lesson.Id,
            Name = lesson.Name,
            Badge = lesson.Badge,
            Description = lesson.Description,
            Category = lesson.Category.Name,
            Type = lesson.Type.Name,
            Status = lesson.Status,
            Progress = userId != 0 ? lesson.LessonUsers
               .FirstOrDefault(user => user.UserId == userId)?.Progress.Name : null,
            Assignments = lesson.Assignments.Select(assignment => new AssignmentAnswer
            {
               Id = assignment.Id,
               Description = assignment.Description,
               Experience = assignment.Experience,
               Progress = userId != 0 ? assignment.AssignmentUsers
                  .Where(user => user.UserId == userId)
                  .FirstOrDefault()?.Progress.Name : null
            }).ToList()
         };
      }

      public async Task<IList<LessonListAnswer>> GetByTypeAllAsync(uint typeId)
      {
         var lessons = await _lessonStore.GetByTypAllAsync(typeId);

         return lessons.Select(
            lesson => new LessonListAnswer
            {
               Id = lesson.Id,
               Name = lesson.Name,
               OwnerId = lesson.Owner.Id,
               OwnerName = lesson.Owner.Name,
               State = lesson.State.Name,
               Badge = lesson.Badge,
               Status = lesson.Status,
               Type = lesson.Type.Name,
               Category = lesson.Category.Name,
               Modified = lesson.Updated ?? lesson.Created
            }).ToList();
      }

      public async Task<IList<LessonListAnswer>> GetByTypePublishedAsync(uint typeId)
      {
         var lessons = await _lessonStore.GetByTypePublishedAsync(typeId);

         return lessons.Select(
            lesson => new LessonListAnswer
            {
               Id = lesson.Id,
               Name = lesson.Name,
               OwnerId = lesson.Owner.Id,
               OwnerName = lesson.Owner.Name,
               State = lesson.State.Name,
               Badge = lesson.Badge,
               Status = lesson.Status,
               Type = lesson.Type.Name,
               Category = lesson.Category.Name,
               Modified = lesson.Updated ?? lesson.Created
            }).ToList();
      }

      public async Task<IList<NameAnswer>> GetCategoriesAsync()
      {
         var categories = await _lessonStore.GetCategoriesAsync();

         return categories.Select(
            category => new NameAnswer
            {
               Id = category.Id,
               Name = category.Name
            }).ToList();
      }

      public async Task<IList<LessonListAnswer>> GetLessonListForAdmin(uint userId)
      {
         var user = await _userStore.GetAsync(userId);
         var answer = new List<LessonListAnswer>();

         if (user == null)
         {
            answer.Add(new LessonListAnswer
            {
               Error = "Your session is not valid..."
            });

            return answer;
         }

         if (user.RoleId != (uint)RoleEnum.Administrator)
         {
            answer.Add(new LessonListAnswer
            {
               Error = "You are not an administrator!"
            });

            return answer;
         }

         var lessons = await _lessonStore.GetLessonListForAdmin();

         return lessons.Select(
            lesson => new LessonListAnswer
            {
               Id = lesson.Id,
               Name = lesson.Name,
               OwnerId = lesson.Owner.Id,
               OwnerName = lesson.Owner.Name,
               State = lesson.State.Name,
               Badge = lesson.Badge,
               Status = lesson.Status,
               Type = lesson.Type.Name,
               Category = lesson.Category.Name,
               Modified = lesson.Updated ?? lesson.Created
            }).ToList();
      }

      public async Task<IList<LessonListAnswer>> GetLessonListForTeacher(uint userId)
      {
         var user = await _userStore.GetAsync(userId);
         var answer = new List<LessonListAnswer>();

         if (user == null)
         {
            answer.Add(new LessonListAnswer
            {
               Error = "Your session is not valid..."
            });

            return answer;
         }

         if (user.RoleId != (uint)RoleEnum.Teacher)
         {
            answer.Add(new LessonListAnswer
            {
               Error = "You are not a teacher!"
            });

            return answer;
         }

         var lessons = await _lessonStore.GetLessonListForOwner(user.Id);

         return lessons.Select(
            lesson => new LessonListAnswer
            {
               Id = lesson.Id,
               Name = lesson.Name,
               OwnerId = lesson.Owner.Id,
               OwnerName = lesson.Owner.Name,
               State = lesson.State.Name,
               Badge = lesson.Badge,
               Status = lesson.Status,
               Type = lesson.Type.Name,
               Category = lesson.Category.Name,
               Description = lesson.Description,
               Modified = lesson.Updated ?? lesson.Created
            }).ToList();
      }

      public async Task<IList<LessonListAnswer>> GetSearchedLesson(uint typeId, string name, uint userId)
      {
         var lessons = await _lessonStore.GetSearchedLesson(userId, typeId, name);

         return lessons.Select(
            lesson => new LessonListAnswer
            {
               Id = lesson.Id,
               Name = lesson.Name,
               OwnerId = lesson.Owner.Id,
               OwnerName = lesson.Owner.Name,
               State = lesson.State.Name,
               Badge = lesson.Badge,
               Status = lesson.Status,
               Type = lesson.Type.Name,
               Category = lesson.Category.Name,
               Modified = lesson.Updated ?? lesson.Created
            }).ToList();
      }

      public async Task<IList<NameAnswer>> GetTypesByCategoryAsync(uint id)
      {
         var types = await _lessonStore.GetTypesByCategoryAsync(id);

         return types.Select(
            type => new NameAnswer
            {
               Id = type.Id,
               Name = type.Name
            }).ToList();
      }

      public async Task<NameAnswer> PostLessonAssignmentAsync(uint lessonId, uint ownerId, string type, IList<AssignmentRequest> assignments)
      {
         var lesson = await _lessonStore.GetAsync(lessonId);

         if (lesson == null)
         {
            return new NameAnswer
            {
               Error = "Lesson not found!"
            };
         }

         if (lesson.OwnerId != ownerId)
         {
            return new NameAnswer
            {
               Error = "Lesson is not owned by you!"
            };
         }

         if (lesson.Type.Name != type)
         {
            return new NameAnswer
            {
               Error = "Lesson is not from the same type!"
            };
         }

         var update = await _lessonStore.PublishLessonAssignmentsAsync(
            lessonId, assignments.Select(assignment => new Assignment
            {
               Id = 0,
               Description = assignment.Description,
               Experience = assignment.Experience,
               Answer = assignment.Answer
            }).ToList());

         return new NameAnswer
         {
            Name = update.Name,
            Id = update.Id
         };
      }

      public async Task<NameAnswer> PostLessonStatusAsync(uint lessonId, bool isValid, string status)
      {
         var lesson = await _lessonStore.GetAsync(lessonId);

         lesson.StateId = isValid ? (uint)Enums.StateEnum.Published : (uint)Enums.StateEnum.Rejected;
         lesson.Status = status;

         var update = await _lessonStore.UpdateLessonStatusAsync(lesson);

         if (update == null)
         {
            return new NameAnswer
            {
               Error = "Failed to update lesson status"
            };
         }

         return new NameAnswer
         {
            Name = update.Name,
            Id = update.Id
         };
      }

      public async Task<NameAnswer> StartLessonAsync(uint lessonId, uint userId)
      {
         var lesson = await _lessonStore.GetAsync(lessonId);

         lesson.LessonUsers = new List<UserLesson>();

         lesson.LessonUsers.Add(new UserLesson
         {
            LessonId = lessonId,
            UserId = userId,
            ProgressId = (uint)ProgressEnum.Active
         });

         foreach (var item in lesson.Assignments)
         {
            item.AssignmentUsers = new List<UserAssignment>();

            item.AssignmentUsers.Add(new UserAssignment
            {
               AssignmentId = item.Id,
               UserId = userId,
               ProgressId = (uint)ProgressEnum.Active
            });
         }

         var update = await _lessonStore.UpdateLessonStatusAsync(lesson);

         if (update == null)
         {
            return new NameAnswer
            {
               Error = "Failed to update assignments"
            };
         }

         return new NameAnswer
         {
            Name = update.Name,
            Id = update.Id
         };
      }

      public async Task<NameAnswer> UpdateLessonAsync(uint typeId, uint ownerId, uint lessonId, string name, string description, byte[] badge)
      {
         var lesson = await _lessonStore.GetAsync(lessonId);

         if (lesson == null)
         {
            return new NameAnswer
            {
               Error = "Lesson was not found!"
            };
         }

         lesson.TypeId = typeId;
         lesson.Name = name;
         lesson.Description = description;

         if (badge.Length != 0)
         {
            lesson.Badge = badge;
         }

         lesson = await _lessonStore.UpdateLessonAsync(lesson);

         return new NameAnswer
         {
            Id = lesson.Id,
            Name = lesson.Name
         };
      }
   }
}