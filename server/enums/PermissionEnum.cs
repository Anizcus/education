namespace Server.Enums
{
   public static class PermissionEnum
   {
      public enum User : uint
      {
         View = 1, Create, Update, Delete, Modify
      }

      public enum Lesson : uint
      {
         View = 11, Create, Update, Delete,
         Authorize
      }
   }
}
