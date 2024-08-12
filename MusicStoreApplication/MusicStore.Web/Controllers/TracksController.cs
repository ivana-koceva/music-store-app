using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.DTO;
using MusicStore.Service.Interface;
using MusicStore.Web.Data;
using MusicStore.Web.Models.Domain;

namespace MusicStore.Web.Controllers
{
    public class TracksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITrackService trackService;
        private readonly IPlaylistService playlistService;

        public TracksController(ApplicationDbContext context, ITrackService trackService, IPlaylistService playlistService)
        {
            _context = context;
            this.trackService = trackService;
            this.playlistService = playlistService;
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tracks.Include(t => t.Album);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackName,Rating,AlbumId,Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                track.Id = Guid.NewGuid();
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName", track.AlbumId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TrackName,Rating,AlbumId,Id")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(Guid id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }

        public IActionResult AddToPlaylist(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var track = trackService.GetDetailsForTrack(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = playlistService.GetAllPlaylists(userId);

            var model = new AddToPlaylistDTO
            {
                TrackId = track.Id,
                TrackName = track.TrackName,
                UserPlaylists = playlists
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToPlaylistConfirmed(AddToPlaylistDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trackInPlaylist = new TrackInPlaylist
            {
                TrackId = model.TrackId,
                UserPlaylistId = model.SelectedPlaylistId
            };
            playlistService.AddToPlaylistConfirmed(trackInPlaylist, userId);

            var tracksWithAlbums = _context.Tracks
                                    .Include(t => t.Album)
                                    .ToList();

            return View("Index", tracksWithAlbums);
        }
    }
}
