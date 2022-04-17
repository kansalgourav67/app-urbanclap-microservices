namespace UrbanClap.NotificationService.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string NotificationMessage { get; set; }
    }
}
