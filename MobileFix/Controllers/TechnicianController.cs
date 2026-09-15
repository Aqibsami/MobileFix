using Microsoft.AspNetCore.Mvc;
using MobileFix.Models;
using MobileFix.Data;
using Microsoft.EntityFrameworkCore;

namespace MobileFix.Controllers;

public class TechnicianController : Controller
{
    private readonly ApplicationDbContext _context;

    public TechnicianController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var workers = await _context.Technicians.ToListAsync();

        return View(workers);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Technician technician)
    {
        if (!ModelState.IsValid)
        {
            return View(technician);
        }

        _context.Technicians.Add(technician);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Edit(int id)
    {
        var technician = await _context.Technicians.FindAsync(id);

        return View(technician);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Technician technician)
    {
        _context.Technicians.Update(technician);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var technician = await _context.Technicians
            .FindAsync(id);

        if (technician == null)
        {
            return NotFound();
        }

        technician.IsActive = !technician.IsActive;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}