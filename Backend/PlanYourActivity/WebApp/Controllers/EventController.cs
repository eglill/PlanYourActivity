using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(a => a.Activity).Include(b => b.Colour).Include(c => c.Creator).Include(d => d.Location);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(c => c.Activity)
                .Include(c => c.Colour)
                .Include(c => c.Creator)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name");
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "Hex");
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartsAt,EndsAt,CreatorId,ActivityId,LocationId,ColourId,Id")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.Id = Guid.NewGuid();
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name", @event.ActivityId);
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "Hex", @event.ColourId);
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "FirstName", @event.CreatorId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", @event.LocationId);
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name", @event.ActivityId);
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "Hex", @event.ColourId);
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "FirstName", @event.CreatorId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", @event.LocationId);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,StartsAt,EndsAt,CreatorId,ActivityId,LocationId,ColourId,Id")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name", @event.ActivityId);
            ViewData["ColourId"] = new SelectList(_context.Colours, "Id", "Hex", @event.ColourId);
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "FirstName", @event.CreatorId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", @event.LocationId);
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(c => c.Activity)
                .Include(c => c.Colour)
                .Include(c => c.Creator)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(Guid id)
        {
          return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
