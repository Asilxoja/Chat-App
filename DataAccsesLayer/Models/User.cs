using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User
    {
        public ObjectId Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class UserDto
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public static implicit operator User(UserDto dto)
        {
            return new User
            {
                Id = ObjectId.Parse(dto.Id),
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password
            };
        }
    }
}
