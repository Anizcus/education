using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Stores.Entities;

namespace Server.Stores.Interfaces
{
   public class UserStore : IUserStore
   {
      private readonly Store _store;
      public UserStore(Store store)
      {
         _store = store;
      }

      public async Task<User> CreateAsync(User entity)
      {
         var added = await _store.Users.AddAsync(entity);
         var saved = await _store.SaveChangesAsync();

         return added.Entity;
      }

      public async Task<User> GetAsync(uint id)
      {
         return await _store.Users
            .FirstOrDefaultAsync(user => user.Id == id);
      }

      public async Task<User> GetProfileAsync(uint id)
      {
         return await _store.Users
            .Include(user => user.Role)
            .Include(user => user.UserLessons)
               .ThenInclude(userLesson => userLesson.Lesson)
            .Include(user => user.UserLessons)
               .ThenInclude(userLesson => userLesson.Lesson)
                  .ThenInclude(lesson => lesson.Type)
            .Include(user => user.UserLessons)
               .ThenInclude(userLesson => userLesson.Lesson)
                  .ThenInclude(lesson => lesson.Category)
            .Include(user => user.UserLessons)
               .ThenInclude(userLesson => userLesson.Lesson)
                  .ThenInclude(lesson => lesson.Owner)
            .Include(user => user.UserLessons)
               .ThenInclude(userLesson => userLesson.Lesson)
                  .ThenInclude(lesson => lesson.State)
            .Include(user => user.UserLessons)
               .ThenInclude(userLesson => userLesson.Progress)
            .FirstOrDefaultAsync(user => user.Id == id);
      }

      public async Task<IList<User>> GetAsync()
      {
         return await _store.Users
            .Include(user => user.Role)
            .ToListAsync();
      }

      public async Task<User> GetAsync(string name)
      {
         return await _store.Users
            .FirstOrDefaultAsync(user => user.Name == name);
      }

      public async Task<IList<Permission>> GetPermissionsAsync(uint roleId)
      {
         var role = await _store.Roles
            .Where(r => r.Id == roleId)
            .Include(r => r.RolePermissions)
               .ThenInclude(rp => rp.Permission)
            .ToListAsync();

         return role
            .Select(r => r.RolePermissions
               .Select(rp => rp.Permission))
            .FirstOrDefault()
            .ToList();
      }

      public bool Any()
      {
         return _store.Users.Any();
      }

      public async Task<IList<Role>> GetRolesAsync()
      {
         return await _store.Roles.ToListAsync();
      }
   }
}
