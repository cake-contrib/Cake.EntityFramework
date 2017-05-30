using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cake.EntityFramework.TestProject.SqlServer.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastSchool { get; set; }

        public string EyeColor { get; set; }

        public int Age { get; set; }

        public int FathersAge { get; set; }

        public string MothersAge { get; set; }

        public string OtherThing { get; set; }

        public ICollection<Class> Class { get; set; }
    }
}