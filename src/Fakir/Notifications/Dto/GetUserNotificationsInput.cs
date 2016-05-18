using Abp.Notifications;
using Fakir.Dto;

namespace Fakir.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}