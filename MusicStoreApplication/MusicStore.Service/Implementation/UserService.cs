using MusicStore.Repository.Interface;
using MusicStore.Service.Interface;
using MusicStore.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<MusicStoreUser> GetAllUsers()
        {
            return userRepository.GetAll();
        }
    }
}
