using System.ComponentModel.DataAnnotations;

namespace MobileFix.Models;

public class Technician
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Specialty { get; set; }

    public bool IsActive { get; set; } = true;
}