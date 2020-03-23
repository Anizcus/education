using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.Attributes
{
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
   public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
   {
      private readonly uint _permission;

      public PermissionAttribute(uint permission)
      {
         _permission = permission;
      }

      public void OnAuthorization(AuthorizationFilterContext context)
      {
         throw new NotImplementedException();
      }
   }
}