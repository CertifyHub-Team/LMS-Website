using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Infra.Repository
{
	public class NotificationRepository:INotificationRepository
	{
		private readonly IDbContext dbContext;
		public NotificationRepository(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void CreateNotification(Notification notification)
		{
			var c = new DynamicParameters();
			c.Add("Section_Id", notification.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			c.Add("Notification_Text", notification.Notificationtext, dbType: DbType.String, direction: ParameterDirection.Input);
			c.Add("Notification_StartDate", notification.Notificationstartdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			c.Add("Notification_EndDate", notification.Notificationenddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Notification_Package.CreateNotification", c, commandType: CommandType.StoredProcedure);

		}

		public void DeleteNotification(int NotificationId)
		{
			var d = new DynamicParameters();
			d.Add("Notification_Id", NotificationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Notification_Package.DeleteNotification", d, commandType: CommandType.StoredProcedure);
		}

		public List<Notification> GetNotificationsByDate()
		{
			var result = dbContext.Connection.Query<Notification>("Notification_Package.GetNotificationsByDate", commandType: CommandType.StoredProcedure);
			return result.ToList();
		}
        public async  Task<List<Notification>> GetNotificationsBySection(int sectionId)
        {
			var p = new DynamicParameters();
			p.Add("Section_Id", sectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await dbContext.Connection.QueryAsync<Notification>("Notification_Package.GetNotificationsBySection", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }


        public void UpdateNotification(Notification notification)
		{
			var u = new DynamicParameters();
			u.Add("Notification_Id",notification.Notificationid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Section_Id", notification.Sectionid, dbType: DbType.Int32, direction: ParameterDirection.Input);
			u.Add("Notification_Text", notification.Notificationtext, dbType: DbType.String, direction: ParameterDirection.Input);
			u.Add("Notification_StartDate", notification.Notificationstartdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			u.Add("Notification_EndDate", notification.Notificationenddate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
			var result = dbContext.Connection.Execute("Notification_Package.UpdateNotification", u, commandType: CommandType.StoredProcedure);
		}
	}
}
