using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coding_Assessment.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required(ErrorMessage ="Please enter your full name")]
        [DisplayName("Name")]
        public string CustomerName { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Please Enter your Address")]
        public string Address { get; set; }
        [DisplayName("Contact Person Name")]
        public string ContactPersonName { get; set; }
       
        [DisplayName("Contact Person Email")]
        [EmailAddress]
        public string Email { get; set; }
   
        [DisplayName("Vat Number")]
        public string VatNumber { get; set; }

        
    }
}
