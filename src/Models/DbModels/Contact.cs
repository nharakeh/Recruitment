using System.ComponentModel.DataAnnotations;
using Vuture.Constants;

namespace Vuture
{
    public class Contact 
    {    
    
        [Key] [Required] public int Id { get; set; }
        
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; } = "";
        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter correct email")]
        public string EmailAddress { get; set; } = "";
        public string? Title { get; set; } = "";
        public string? Company { get; set; } = "";
        public string? Status { get; set; } = ContactStatus.Lead;

    }
}