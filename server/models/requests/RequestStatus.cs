namespace Server.Models.Requests
{
   public class StatusRequest
   {
      public uint LessonId { get; set; }
      public bool IsValid { get; set; }
      public string Status { get; set; }
   }
}
