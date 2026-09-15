using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileFix.Data;

namespace MobileFix.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalRepairs = await _context.RepairTickets.CountAsync();

        var pending = await _context.RepairTickets
            .CountAsync(x => x.Status == "Pending");

        var inProgress = await _context.RepairTickets
            .CountAsync(x => x.Status == "In Progress");

        var completed = await _context.RepairTickets
            .CountAsync(x =>
                x.Status == "Completed" ||
                x.Status == "Delivered");

        var recentRepairs = await _context.RepairTickets
            .Include(x => x.Technician)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        ViewBag.TotalRepairs = totalRepairs;
        ViewBag.Pending = pending;
        ViewBag.InProgress = inProgress;
        ViewBag.Completed = completed;
        ViewBag.RecentRepairs = recentRepairs;

        return View();
    }
}