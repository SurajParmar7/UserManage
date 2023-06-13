using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.entities.Viewmodels
{
    public class UserAddview
    {  
        [Key]
        public long UserId { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid e-mail address")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        ErrorMessage = "Please enter valid phone number")]
        [MaxLength(50)]
        public string? Phone { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "StreetAddress must below  150 characters")]

        public string? StreetAddress { get; set; }
        public long? City { get; set; }    
        public long? State { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]

        public string? Password { get; set; }



    }
}
