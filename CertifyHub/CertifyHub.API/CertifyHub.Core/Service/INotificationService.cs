using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
	public interface INotificationService
	{
		public List<Notification> GetNotificationsByDate();
		public void CreateNotification(Notification notification);
		public void DeleteNotification(int NotificationId);
		public void UpdateNotification(Notification notification);
		public Task<List<Notification>> GetNotificationsBySection(int sectionId);

    }
}
