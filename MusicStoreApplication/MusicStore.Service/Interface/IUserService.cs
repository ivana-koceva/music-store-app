using MusicStore.Web.Models.Domain;
using MusicStore.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Service.Interface
{
    public interface IUserService
    {
        List<MusicStoreUser> GetAllUsers();
    }
}
