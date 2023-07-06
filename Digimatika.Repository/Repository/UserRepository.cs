using Dapper;
using Digimatika.Core.Models;
using Digimatika.Repository.Interface.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Digimatika.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private string connection;

        public UserRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("sqlConnection");
        }

        public async Task<bool> Add(User user)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                Guid guid = Guid.NewGuid();
                var query = @"INSERT INTO Users (Id,Name, Email, PhoneNumber, PasswordHash, Status, CreatedAt)
                            VALUES (NEWID(),@Name, @Email, @PhoneNumber,HASHBYTES('SHA',@PasswordHash) ,'Active', GETDATE())";
                await conn.QueryAsync(query, user);
                return true;
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"SELECT *
                            FROM Users";
                var queryStatement = await conn.QueryAsync<User>(query);
                return queryStatement.ToList();
            }
        }

        public async Task<User> Get(string id)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"SELECT Id, Name, Email, PhoneNumber, PasswordHash
                            FROM Users
                            WHERE Id = @Id";
                var queryStatement = await conn.QueryFirstOrDefaultAsync<User>(query,new {@Id = id});
                return queryStatement;
            }
        }

        public async Task<bool> Update(User user, string id)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var ids = id;
                //var newValue = new { @Id = Convert.ToInt32(id.ToString()), @Name = user.Name, @Email = user.Email, @PhoneNumber = user.PhoneNumber, @PasswordHash = user.PasswordHash };
                var newValue = new { @Id = id, @Name = user.Name, @Email = user.Email, @PhoneNumber = user.PhoneNumber, @PasswordHash = user.PasswordHash };
                var query = @"UPDATE Users
                            SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber , PasswordHash = HASHBYTES('SHA',@PasswordHash), UpdatedAt = GETDATE()
                            WHERE Id = @Id";
                await conn.QuerySingleOrDefaultAsync<User>(query, newValue);
                return true;
            }
        }

        public async Task<bool> Delete(User user, string id)
        {
            using (var conn = new SqlConnection(connection))
            {
                await conn.OpenAsync();
                var query = @"UPDATE Users
                            SET Status = 'Inactive', UpdatedAt = GETDATE()
                            WHERE Id=@Id";
                await conn.QuerySingleOrDefaultAsync<User>(query, new{ @Id=id});
                return true;
            }
        }
    }
}
