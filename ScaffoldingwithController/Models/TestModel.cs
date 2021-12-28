using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScaffoldingwithController.Models
{
    [Table("Test1", Schema = "public")]
    public class TestModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [StringLength(10,ErrorMessage ="Limit is up to 10")]
        [Display(Name = "First Name")]
        public string Name { get; set; }


        [DataType(DataType.PhoneNumber,ErrorMessage ="Please enter only number")]
        [MaxLength(10,ErrorMessage ="Please enter on 10 digitnumber")]
   
        //[RegularExpression(@"[^0-9.-]+", ErrorMessage = "Use Number only please")]

        public string Mobile { get; set; }  

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-MM}", ApplyFormatInEditMode = true)]

        public DateTime Datetime { get; set; }

        [Required(ErrorMessage = "Email is requied")]
        [DataType(DataType.EmailAddress, ErrorMessage = "please enter email in correct formate") ]
        public string Email { get; set; }
    }
}
