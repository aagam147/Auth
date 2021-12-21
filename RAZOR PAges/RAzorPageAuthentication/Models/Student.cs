using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RAzorPageAuthentication.Models
{
    [Table("Student",Schema ="public")]
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
