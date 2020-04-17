namespace Server.Models.Payloads
{
   public class LessonListPayload
   {
      public uint Id { get; set; }
      public string Name { get; set; }
      public uint OwnerId { get; set; }
      public string OwnerName { get; set; }
      public string State { get; set; }
      public string BadgeBase64 { get; set; }
   }
}
