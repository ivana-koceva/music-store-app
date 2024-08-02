using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Web.Data;
using MusicStore.Web.Models.Domain;

namespace MusicStore.Web.Controllers
{
    public class UserPlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserPlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserPlaylists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Playlists.Include(u => u.Owner).Include(u => u.TracksInPlaylist).ThenInclude(t => t.Track);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserPlaylists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.Playlists
                .Include(u => u.Owner).Include(u => u.Owner).Include(u => u.TracksInPlaylist).ThenInclude(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            return View(userPlaylist);
        }

        // GET: UserPlaylists/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserPlaylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] UserPlaylist userPlaylist)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            userPlaylist.Id = Guid.NewGuid();
            userPlaylist.OwnerId = userId;

            if (userPlaylist.Id != null && userPlaylist.OwnerId != null && userPlaylist.Name != null)
            {
                _context.Add(userPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", userPlaylist.OwnerId);
            return View(userPlaylist);
        }

        // GET: UserPlaylists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.Playlists.FindAsync(id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            if (userPlaylist.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", userPlaylist.OwnerId);
            return View(userPlaylist);
        }

        // POST: UserPlaylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,OwnerId,Id")] UserPlaylist userPlaylist)
        {
            if (id != userPlaylist.Id)
            {
                return NotFound();
            }

            // Retrieve the existing playlist
            var existingPlaylist = await _context.Playlists.FindAsync(id);

            // Check if the playlist exists and if the user is the owner
            if (existingPlaylist == null || existingPlaylist.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            // Update properties on the existing playlist
            existingPlaylist.Name = userPlaylist.Name;

            if (existingPlaylist.OwnerId != null && existingPlaylist.Name != null)
            {
                try
                {
                    _context.Update(existingPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPlaylistExists(existingPlaylist.Id))
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

            // Log or display model state errors
            foreach (var modelState in ModelState)
            {
                var errors = modelState.Value.Errors;
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // Populate ViewData to maintain the state of the OwnerId
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", existingPlaylist.OwnerId);
            return View(existingPlaylist);
        }

        // GET: UserPlaylists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPlaylist = await _context.Playlists
                .Include(u => u.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPlaylist == null)
            {
                return NotFound();
            }

            if (userPlaylist.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            return View(userPlaylist);
        }

        // POST: UserPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userPlaylist = await _context.Playlists.FindAsync(id);
            if (userPlaylist != null)
            {
                if (userPlaylist.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return Forbid();
                }

                _context.Playlists.Remove(userPlaylist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlaylistExists(Guid id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }
    }
}
