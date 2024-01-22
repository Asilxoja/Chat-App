using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class LoginUser
    {
        public string PhoneNumber { get; set; } = null!;
        public required string Password { get; set; }
    }
}
