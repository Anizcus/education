namespace Server.Enums
{
   public static class PermissionEnum
   {
      public enum User : uint
      {
         View = 1, Create, Update, Delete
      }

      public enum Lesson : uint
      {
         View = 11, Create, Update, Delete
      }
   }
}
