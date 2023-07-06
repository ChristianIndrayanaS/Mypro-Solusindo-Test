using Digimatika.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Digimatika.Services.Interface.Interface
{
    public interface IUserServices
    {
        public Task<bool> AddUser(User user);
        public Task<List<User>> GetAllUser();
        public Task<bool> UpdateUser(User user, string id);
        public Task<User> GetUser(string id);
        public Task<bool> DeleteUser(User user, string id);
    }
}
