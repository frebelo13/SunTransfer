using System.ComponentModel.DataAnnotations;

namespace SunTransfersApp.Models
{
    public class Client
    {
        [Required(ErrorMessage = "The clientId is required.")]
        public Guid clientId { get; set; }
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, ErrorMessage = "The name must be at most 50 characters.")]
        public string name { get; set; }
        [Required(ErrorMessage = "The surname is required.")]
        [StringLength(50, ErrorMessage = "The surname must be at most 50 characters.")]
        public string surname { get; set; }
        [Required(ErrorMessage = "The age is required.")]
        [Range(18, 120, ErrorMessage = "The age must be between 18 and 120.")]
        public int age { get; set; }
        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email is not valid.")]
        public string email { get; set; }
    }
}
