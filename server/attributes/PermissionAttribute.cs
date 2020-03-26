using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
         var permission = context.HttpContext.User.Claims
            .Where(c => c.Type == "ups" && c.Value == _permission.ToString())
            .FirstOrDefault();

         if (permission == null)
         {
            context.Result = new StatusCodeResult((int) HttpStatusCode.Forbidden);
         }
      }
   }
}