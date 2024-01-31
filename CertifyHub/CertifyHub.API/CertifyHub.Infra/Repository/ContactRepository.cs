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
    public class ContactRepository : IContactRepository
    {
        private readonly IDbContext _dbContext;

        public ContactRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async void CreateContact(Contact contact)
        {
            var p = new DynamicParameters();
            p.Add("pName", contact.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("pEmail", contact.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("pPhoneNumber", contact.Phonenumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("pMessage", contact.Message, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("pContact_Date", contact.Contactdate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
          await  _dbContext.Connection.ExecuteAsync("Contact_Package.CreateContact", p, commandType: CommandType.StoredProcedure);

        }

        public async void DeleteContact(int id)
        {
            var p = new DynamicParameters();
            p.Add("Contact_Id", id, dbType: DbType.Int32, ParameterDirection.Input);
          await  _dbContext.Connection.ExecuteAsync("Contact_Package.DeleteContact", p, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<Contact>> GetAllContact()
        {
            var result = await _dbContext.Connection.QueryAsync<Contact>("Contact_Package.GetAllContact", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }
    }
}
