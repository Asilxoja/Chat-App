using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DataAccsesLayer.Models;

public class User
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
