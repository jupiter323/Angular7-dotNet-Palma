using Abp.Notifications;
using PALMASoft.Dto;

namespace PALMASoft.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}