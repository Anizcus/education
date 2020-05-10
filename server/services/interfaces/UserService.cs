using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Enums;
using Server.Services.Answers;
using Server.Stores.Entities;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class UserService : IUserService
   {
      private readonly IUserStore _userStore;
      private readonly IConfiguration _configuration;
      private readonly JwtSecurityTokenHandler _tokenHandler;

      public UserService(IUserStore userStore, IConfiguration configuration)
      {
         _userStore = userStore;
         _configuration = configuration;
         _tokenHandler = new JwtSecurityTokenHandler();
      }
      public async Task<NameAnswer> GetAsync(uint id)
      {
         var user = await _userStore.GetAsync(id);

         if (user == null)
         {
            return new NameAnswer
            {
               Error = "User not found!"
            };
         }

         return new NameAnswer
         {
            Name = user.Name
         };
      }

      public async Task<SessionAnswer> SignInAsync(string username, string password)
      {
         if (String.IsNullOrEmpty(username) || username.Length > 64)
         {
            return new SessionAnswer
            {
               Error = "Username value must be valid and less / equal 64 characters"
            };
         }

         if (String.IsNullOrEmpty(password) || password.Length > 64 || password.Length < 8)
         {
            return new SessionAnswer
            {
               Error = "Password value must be valid and between 8 and 64 characters"
            };
         }

         var user = await _userStore.GetAsync(username);

         if (user == null)
         {
            return new SessionAnswer
            {
               Error = "User does not exist!"
            };
         }

         var combine = CombineWithSalt(username, password, user.Salt);
         var hash = SecureWithSHA512(combine);

         if (!hash.SequenceEqual(user.Password))
         {
            return new SessionAnswer
            {
               Error = "Wrong password / username!"
            };
         }

         if (user.Blocked == true) {
            return new SessionAnswer
            {
               Error = "Sorry, but you are blocked by administrator."
            };
         }

         var permissions = await _userStore.GetPermissionsAsync(user.RoleId);
         var token = CreateToken(user, permissions);

         return new SessionAnswer
         {
            Id = user.Id,
            Name = user.Name,
            Session = WriteToken(token)
         };
      }

      public async Task<NameAnswer> SignUpAsync(string username, string password, uint role)
      {
         if (String.IsNullOrEmpty(username) || username.Length > 64)
         {
            return new NameAnswer
            {
               Error = "Username value must be valid and less / equal 64 characters"
            };
         }

         if (String.IsNullOrEmpty(password) || password.Length > 64 || password.Length < 8)
         {
            return new NameAnswer
            {
               Error = "Password value must be valid and between 8 and 64 characters"
            };
         }

         var user = await _userStore.GetAsync(username);

         if (user != null)
         {
            return new NameAnswer
            {
               Error = "User does exist!"
            };
         }

         var salt = SecureWithSHA512(GetRandomSalt());
         var combine = CombineWithSalt(username, password, salt);
         var hash = SecureWithSHA512(combine);

         user = new User
         {
            Name = username,
            Salt = salt,
            Password = hash,
            RoleId = role
         };

         user = await _userStore.CreateAsync(user);

         if (user == null)
         {
            return new NameAnswer
            {
               Error = "User registration has failed"
            };
         }

         return new NameAnswer
         {
            Id = user.Id,
            Name = user.Name
         };
      }

      public Task<SessionAnswer> RestoreAsync(string session, string account)
      {
         throw new System.NotImplementedException();
      }

      private byte[] GetRandomSalt(int length = 128)
      {
         byte[] input = new byte[length];

         using (var rng = new RNGCryptoServiceProvider())
         {
            rng.GetNonZeroBytes(input);
         }

         return input;
      }

      private byte[] SecureWithSHA512(byte[] input)
      {
         using (var sha512 = SHA512.Create())
         {
            return sha512.ComputeHash(input);
         }
      }

      private byte[] CombineWithSalt(string username, string password, byte[] salt)
      {
         var name = Encoding.UTF8.GetBytes(username);
         var word = Encoding.UTF8.GetBytes(password);

         return name.Concat(salt).Concat(word).ToArray();
      }

      public SecurityToken CreateToken(User user, IList<Permission> permissions)
      {
         var time = TimeSpan.Parse(_configuration["Token:Lifetime"]);
         var key = Encoding.UTF8.GetBytes(_configuration["Token:Secret"]);

         var tokenClaims = new ClaimsIdentity(
            new[] {
               new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

         foreach (Permission p in permissions)
         {
            tokenClaims.AddClaim(
               new Claim("ups", p.Id.ToString())
            );
         }

         var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature
         );

         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = tokenClaims,
            Audience = _configuration["Token:Audience"],
            Issuer = _configuration["Token:Issuer"],
            Expires = DateTime.UtcNow.Add(time),
            SigningCredentials = credentials
         };

         return _tokenHandler.CreateToken(tokenDescriptor);
      }
      public string WriteToken(SecurityToken token)
      {
         return _tokenHandler.WriteToken(token);
      }

      public async Task<NameAnswer> GetAsync(IEnumerable<Claim> claims)
      {
         var subject = claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .FirstOrDefault();

         if (subject == null)
         {
            return new NameAnswer
            {
               Error = "Session has no subject"
            };
         }

         var user = await _userStore.GetAsync(Convert.ToUInt32(subject.Value));

         if (user == null)
         {
            return new NameAnswer
            {
               Error = "User does not exist!"
            };
         }

         return new NameAnswer
         {
            Id = user.Id,
            Name = user.Name
         };
      }

      public async Task<ProfileAnswer> GetProfileAsync(uint userId)
      {
         var user = await _userStore.GetProfileAsync(userId);

         if (user == null)
         {
            return new ProfileAnswer
            {
               Error = "User not found!"
            };
         }

         return new ProfileAnswer
         {
            Id = user.Id,
            Name = user.Name,
            NextExperience = (user.Level + 1) * 15,
            Level = user.Level,
            Role = user.Role.Name,
            Experience = user.Experience,
            Lessons = user.UserLessons.Select(lesson => new LessonAnswer
            {
               Id = lesson.Lesson.Id,
               Name = lesson.Lesson.Name,
               Description = lesson.Lesson.Description,
               OwnerId = lesson.Lesson.OwnerId,
               OwnerName = lesson.Lesson.Owner.Name,
               Type = lesson.Lesson.Type.Name,
               State = lesson.Lesson.State.Name,
               Badge = lesson.Lesson.Badge,
               Status = lesson.Lesson.Status,
               Category = lesson.Lesson.Category.Name,
               Progress = lesson.Progress.Name
            }).ToList()
         };
      }

      public async Task<IList<UserListAnswer>> GetAsync()
      {
         var users = await _userStore.GetAsync();

         if (users == null)
         {
            return new List<UserListAnswer>();
         }

         return users.Select(user => new UserListAnswer
         {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role.Name,
            Level = user.Level
         }).ToList();
      }

      public async Task<IList<NameAnswer>> GetRolesForRegisterAsync()
      {
         if (_userStore.Any())
         {
            var roles = await _userStore.GetRolesAsync();

            return roles
               .Where(role => role.Id != (uint)RoleEnum.Administrator)
               .Select(role => new NameAnswer
               {
                  Id = role.Id,
                  Name = role.Name
               }).ToList();
         }
         else
         {
            var administrator = new NameAnswer
            {
               Id = (uint)RoleEnum.Administrator,
               Name = nameof(RoleEnum.Administrator)
            };

            var list = new List<NameAnswer> { administrator };

            return list;
         }
      }

      public async Task<IList<NameAnswer>> GetRolesAsync()
      {
         var roles = await _userStore.GetRolesAsync();

         return roles.Select(role => new NameAnswer
         {
            Id = role.Id,
            Name = role.Name
         }).ToList();
      }

      public async Task<NameAnswer> PostModifyUserStatusAsync(uint userId, uint role, bool isBlocked)
      {
         var user = await _userStore.GetAsync(userId);

         user.RoleId = role;
         user.Blocked = isBlocked;

         var updated = await _userStore.UpdateAsync(user);

         if (updated == null)
         {
            return new NameAnswer
            {
               Error = "Failed to update user status."
            };
         }

         return new NameAnswer
         {
            Id = updated.Id,
            Name = updated.Name
         };
      }
   }
}