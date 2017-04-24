using System.Collections.Generic;

namespace Cake.EntityFramework.TestProject.Postgres.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastSchool { get; set; }

        public string EyeColor { get; set; }

        public int Age { get; set; }

        public int FatherAge { get; set; }

        public int MotherAge { get; set; }

        public string OtherThing { get; set; }

        public ICollection<Class> Class { get; set; }
    }
}