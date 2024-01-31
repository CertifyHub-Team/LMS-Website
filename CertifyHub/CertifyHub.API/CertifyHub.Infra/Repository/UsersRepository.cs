using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertifyHub.Core.Common;
using CertifyHub.Core.Data;
using CertifyHub.Core.Repository;
using Dapper;
using static System.Collections.Specialized.BitVector32;

namespace CertifyHub.Infra.Repository
{
    public class UsersRepository : IUsersRepository
    {


        private readonly IDbContext _dbContext;
        public UsersRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateUser(User user)
        {
            var p = new DynamicParameters();
            p.Add("FIRSTNAME", user.Firstname, DbType.String, ParameterDirection.Input);
            p.Add("LASTNAME", user.Lastname, DbType.String, ParameterDirection.Input);
            p.Add("DATEOFBIRTH", user.Dateofbirth, DbType.DateTime, ParameterDirection.Input);
            p.Add("IMAGEPATH", user.Imagepath, DbType.String, ParameterDirection.Input);
            p.Add("ADDRESS", user.Address, DbType.String, ParameterDirection.Input);
            p.Add("PHONENUMBER", user.Phonenumber, DbType.String, ParameterDirection.Input);
            p.Add("REGISTRATIONDATE", user.Registrationdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("ISACTIVE", user.Isactive, DbType.Int32, ParameterDirection.Input);
            p.Add("CREATED_USERID", dbType: DbType.Int32, direction: ParameterDirection.Output); // Define the output parameter

            await _dbContext.Connection.ExecuteAsync("USER_PACKAGE.CREATE_USER", p, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value
            int createdUserId = p.Get<int>("CREATED_USERID");

            return createdUserId;
        }


        public async void DeleteUser(int id)
        {
            var p = new DynamicParameters();
            p.Add("USER_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

           await _dbContext.Connection.ExecuteAsync("USER_PACKAGE.DELETE_USER", p, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<User>> GetAllUsers()
        {

           var result = await _dbContext.Connection.QueryAsync<User>("USER_PACKAGE.GET_ALL_USERS", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<User>> GetAllStudents()
        {

            var result = await _dbContext.Connection.QueryAsync<User, Login, User>("USER_PACKAGE.GET_ALL_STUDENTS", 
                (user, login) =>
                {
                    user.Logins ??= new List<Login>();
                    user.Logins.Add(login);
                    return user;
                },
                splitOn: "userid",
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<List<User>> GetAllInstructors()
        {

            var result = await _dbContext.Connection.QueryAsync<User, Login, User>("USER_PACKAGE.GET_ALL_INSTRUCTORS",
                (user, login) =>
                {
                    user.Logins ??= new List<Login>();
                    user.Logins.Add(login);
                    return user;
                },
                splitOn: "userid",
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }


        public async Task<User> GetUserById(int id)
        {
            var p = new DynamicParameters();
            p.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<User>("User_Package.GET_USER_BY_ID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();

        }

        public async void UpdateUserImage(int userId, string newImagePath)
        {
            var p = new DynamicParameters();
            p.Add("USER_ID", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("NEW_IMG", newImagePath, dbType: DbType.String, direction: ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("USER_PACKAGE.UPDATE_USER_IMAGE", p, commandType: CommandType.StoredProcedure);
        }

        public async void UpdateUserIsActive(int userId, int isActive)
        {
            var p = new DynamicParameters();
            p.Add("USER_ID", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("IS_ACTIVE", isActive, dbType: DbType.Int32, direction: ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("USER_PACKAGE.UPDATE_ISACTIVE_USER", p, commandType: CommandType.StoredProcedure);
        }

        public async void UpdateUserPhone(int userId, string newPhoneNumber)
        {

            var p = new DynamicParameters();
            p.Add("USER_ID", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("NEW_PHONE", newPhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);

          await  _dbContext.Connection.ExecuteAsync("USER_PACKAGE.UPDATE_USER_PHONE", p, commandType: CommandType.StoredProcedure);
        }
       public async void UpdateUser(User user)
       {

            var p = new DynamicParameters();
            p.Add("USER_ID", user.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("FIRST_NAME", user.Firstname, DbType.String, ParameterDirection.Input);
            p.Add("LAST_NAME", user.Lastname, DbType.String, ParameterDirection.Input);
            p.Add("DATE_OF_BIRTH", user.Dateofbirth, DbType.DateTime, ParameterDirection.Input);
            p.Add("IMAGE_PATH", user.Imagepath, DbType.String, ParameterDirection.Input);
            p.Add("ADDRESS_", user.Address, DbType.String, ParameterDirection.Input);
            p.Add("PHONE_NUMBER", user.Phonenumber, DbType.String, ParameterDirection.Input);
            p.Add("REGISTRATION_DATE", user.Registrationdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("IS_ACTIVE", user.Isactive, DbType.Int32, ParameterDirection.Input);

            await _dbContext.Connection.ExecuteAsync("USER_PACKAGE.UPDATE_USER", p, commandType: CommandType.StoredProcedure);
       }
        public async Task<List<User>> GetStudentsCvs()
        {

            var result = await _dbContext.Connection.QueryAsync<User>("USER_PACKAGE.GET_STUDENTS_WITH_CV", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }


}




