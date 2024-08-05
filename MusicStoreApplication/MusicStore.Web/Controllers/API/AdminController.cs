using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AdminController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
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


    }
}
