using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProject.Models
{
    [Table("Test2",Schema ="publc")]
    public class Test
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
