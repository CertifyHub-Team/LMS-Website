using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using CertifyHub.Core.Service;
using CertifyHub.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Service
{
	public class NotificationService:INotificationService
	{
		private readonly INotificationRepository notificationRepository;

		public NotificationService(INotificationRepository notificationRepository)
		{
			this.notificationRepository = notificationRepository;
		}

		public void CreateNotification(Notification notification)
		{
			notificationRepository.CreateNotification(notification);
		}

		public void DeleteNotification(int NotificationId)
		{
			notificationRepository.DeleteNotification(NotificationId);
		}

		public List<Notification> GetNotificationsByDate()
		{
			return notificationRepository.GetNotificationsByDate();
		}
        public async Task<List<Notification>> GetNotificationsBySection(int sectionId)
		{
			return await notificationRepository.GetNotificationsBySection(sectionId);

        }

        public void UpdateNotification(Notification notification)
		{
			notificationRepository.UpdateNotification(notification);
		}
	}
}
