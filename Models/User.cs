using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Bio { get; set; }

        public string PictureURL { get; set; }

        public int UserRole { get; set; }

        [InverseProperty("User")]
        public ICollection<IndividualTask>? Tasks { get; set; }
    }

}
