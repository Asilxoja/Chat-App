using System.ComponentModel.DataAnnotations;

namespace DataAccsesLayer.Models;

public class LoginUser
{
    public required string PhoneNumber { get; set; } = null!;
    public required string Password { get; set; } = null!;
}
 