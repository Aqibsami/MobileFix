using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileFix.Data;
using MobileFix.Models;

namespace MobileFix.Controllers;

public class RepairTicketController : Controller
{
    private readonly ApplicationDbContext _context;

    public RepairTicketController(ApplicationDbContext context)
    {
        _context = context;
    }


    // =========================
    // INDEX
    // =========================
    public async Task<IActionResult> Index()
    {
        var tickets = await _context.RepairTickets
            .Include(t => t.Technician)
            .ToListAsync();

        return View(tickets);
    }


    // =========================
    // DETAILS - GET
    // =========================
    public async Task<IActionResult> Details(int id)
    {
        var ticket = await _context.RepairTickets
            .Include(t => t.Technician)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }


    // =========================
    // CREATE - GET
    // =========================
    public async Task<IActionResult> Create()
    {
        ViewBag.Technicians = await _context.Technicians
    .Where(t => t.IsActive)
    .ToListAsync();

        return View();
    }


    // =========================
    // CREATE - POST
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create(RepairTicket ticket)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Technicians = await _context.Technicians
                .ToListAsync();

            return View(ticket);
        }

        ticket.Token =
            $"MF-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

        ticket.CreatedAt = DateTime.Now;

        _context.RepairTickets.Add(ticket);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    // =========================
    // EDIT - GET
    // =========================
    public async Task<IActionResult> Edit(int id)
    {
        var ticket = await _context.RepairTickets
            .FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        ViewBag.Technicians = await _context.Technicians
            .ToListAsync();

        return View(ticket);
    }


    // =========================
    // EDIT - POST
    // =========================
    [HttpPost]
    public async Task<IActionResult> Edit(int id, RepairTicket ticket)
    {
        if (id != ticket.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Technicians = await _context.Technicians
    .Where(t => t.IsActive)
    .ToListAsync();

            return View(ticket);
        }

        _context.RepairTickets.Update(ticket);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    // =========================
    // DELETE - GET
    // =========================
    public async Task<IActionResult> Delete(int id)
    {
        var ticket = await _context.RepairTickets
            .Include(t => t.Technician)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }


    // =========================
    // DELETE - POST
    // =========================
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ticket = await _context.RepairTickets
            .FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        _context.RepairTickets.Remove(ticket);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Search(string query)
    {
        var ticketsQuery = _context.RepairTickets
            .Include(t => t.Technician)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.Trim();

            ticketsQuery = ticketsQuery.Where(t =>
                t.Token.Contains(query) ||
                t.CustomerName.Contains(query) ||
                t.CustomerPhone.Contains(query) ||
                t.DeviceType.Contains(query) ||
                t.Brand.Contains(query) ||
                t.Model.Contains(query) ||
                t.IMEI.Contains(query) ||
                t.Issue.Contains(query) ||
                t.Status.Contains(query) ||
                (t.Technician != null && t.Technician.Name.Contains(query))
            );
        }

        var tickets = await ticketsQuery
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        ViewBag.Query = query;

        return View(tickets);
    }
}