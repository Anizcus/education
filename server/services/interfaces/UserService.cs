using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Server.Services.Answers;
using Server.Stores.Entities;
using Server.Stores.Interfaces;

namespace Server.Services.Interfaces
{
   public class UserService : IUserService
   {
      private readonly IUserStore _userStore;

      public UserService(IUserStore userStore)
      {
         _userStore = userStore;
      }
      public async Task<NameAnswer> GetAsync(int id)
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

         if (hash.SequenceEqual(user.Password)) {
            return new SessionAnswer
            {
               Error = "You have logged in"
            };
         }

         return new SessionAnswer
         {
            Error = "End of method"
         };
      }

      public async Task<NameAnswer> SignUpAsync(string username, string password)
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
            Password = hash
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
         var name = System.Text.Encoding.UTF8.GetBytes(username);
         var word = System.Text.Encoding.UTF8.GetBytes(password);

         return name.Concat(salt).Concat(word).ToArray();
      }
   }
}