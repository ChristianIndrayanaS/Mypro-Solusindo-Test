using Digimatika.Core.Models;
using System.Reflection.Metadata;

namespace Digimatika.Repository.Interface.Interface
{
    public interface IUserRepository
    {
        public Task<bool> Add(User user);
        //public Task<bool> Delete(int id);
        public Task<List<User>> GetAll();
        public Task<bool> Update(User user, string id);
        public Task<User> Get(string id);
        public Task<bool> Delete(User user,string id);
    }
}
