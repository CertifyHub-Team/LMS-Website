using CertifyHub.Core.Data;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertifyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService notificationService;
		public NotificationController(INotificationService notificationService)
		{
			this.notificationService = notificationService;
		}

		[HttpGet]
		[Route("GetNotificationsByDate")]
		public List<Notification> GetNotificationsByDate()
		{
			return notificationService.GetNotificationsByDate();
		}
		[HttpPost]
		[Route("CreateNotification")]
		public void CreateNotification(Notification notification)
		{
			notificationService.CreateNotification(notification);
		}

		[HttpPut]
		[Route("UpdateNotification")]
		public void UpdateNotification(Notification notification)
		{
			notificationService.UpdateNotification(notification);
		}
		[HttpDelete]
		[Route("DeleteNotification/{NotificationId}")]
		public void DeleteNotification(int NotificationId)
		{
			notificationService.DeleteNotification(NotificationId);
		}
        [HttpGet]
        [Route("GetNotificationsBySection/{sectionId}")]
        public Task<List<Notification>> GetNotificationsBySection(int sectionId)
		{
			return notificationService.GetNotificationsBySection(sectionId);
		}


    }
}
