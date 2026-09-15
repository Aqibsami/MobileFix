using System.ComponentModel.DataAnnotations;

namespace MobileFix.Models;

public class RepairTicket
{
    public int Id { get; set; }

    public string? Token { get; set; }

    [Required]
    public string CustomerName { get; set; }

    public string CustomerPhone { get; set; }

    [Required]
    public string DeviceType { get; set; }

    [Required]
    public string Brand { get; set; }

    [Required]
    public string Model { get; set; }

    public string IMEI { get; set; }

    [Required]
    public string Issue { get; set; }

    [Required]
    public string Status { get; set; }

    [Range(0, double.MaxValue)]
    public decimal EstimatedCost { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? TechnicianId { get; set; }

    public Technician? Technician { get; set; }
}