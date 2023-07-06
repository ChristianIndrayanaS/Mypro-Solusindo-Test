using Digimatika.Core.Models;
using Digimatika.Repository.Interface.Interface;
using Digimatika.Services.Interface.Interface;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Digimatika.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(User user)
        {
            return await _userRepository.Add(user);
        }
        public async Task<List<User>> GetAllUser()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetUser(string id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<bool> UpdateUser(User user, string id)
        {
            return await _userRepository.Update(user, id);
        }

        public async Task<bool> DeleteUser(User user,string id)
        {
            return await _userRepository.Delete(user,id);
        }
    }
}
