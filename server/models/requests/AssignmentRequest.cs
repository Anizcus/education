namespace Server.Models.Requests
{
   public class AssignmentRequest
   {
      public uint Id { get; set; }
      public string Description { get; set; }
      public uint Experience { get; set; }
      public string Answer { get; set; }
   }
}