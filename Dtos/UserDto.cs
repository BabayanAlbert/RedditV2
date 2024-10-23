using Reddit.Models;
using System.ComponentModel.DataAnnotations;

namespace Reddit.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect Email Address")]
        public string Email { get; set; }
        public User CreateUser()
        {
            return new User
            {
                Name = Name,
                Email = Email
            };
        }
    }
}
