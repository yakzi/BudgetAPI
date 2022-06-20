using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public User(string name, string password, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Password = password;
            Email = email;
            CreatedDate = DateTime.Now;
        }
    }
}
