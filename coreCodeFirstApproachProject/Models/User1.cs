using System.ComponentModel.DataAnnotations;

namespace coreCodeFirstApproachProject.Models
{
    public class User1
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
