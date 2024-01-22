using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DataAccsesLayer.Models;

public class User
{
    [Key, Required]
   public int Id { get; set; }
    [Required]
    public required string FullName { get; set; } = null!;
    [Required]
    public required string PhoneNumber { get; set; } = null!;
    [Required]
    public required string Password { get; set; } = null!;
}
