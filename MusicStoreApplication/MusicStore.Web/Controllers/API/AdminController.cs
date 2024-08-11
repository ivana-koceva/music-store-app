using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Domain.DTO;
using MusicStore.Service.Interface;
using MusicStore.Web.Models.Domain;
using MusicStore.Web.Models.Identity;
using System.Security.Claims;

namespace MusicStore.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly UserManager<MusicStoreUser> _userManager;
        private readonly IUserService _userService;

        public AdminController(IPlaylistService playlistService, UserManager<MusicStoreUser> userManager,
            IUserService userService)
        {
            _playlistService = playlistService;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet("[action]")]
        public List<MusicStoreUser> GetAllUsers()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return this._userService.GetAllUsers();
        }

        [HttpGet("[action]")]
        public List<UserPlaylist> GetAllPlaylists() 
        {
           // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return this._playlistService.GetAllPlaylists();
        }

        [HttpPost("[action]")]
        public UserPlaylist GetDetails(BaseEntity id)
        {
            return this._playlistService.GetDetailsForPlaylist(id);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new MusicStoreUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                    };

                    var result = _userManager.CreateAsync(user, item.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }


    }
}
