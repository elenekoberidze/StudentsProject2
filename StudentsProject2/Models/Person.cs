using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsProject2.Models
{
    public abstract class Person
    {
        public string Name { get; set; } = string.Empty;
        protected Person(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            this.Name = name.Trim();
        }

        protected Person() { }
       

        public abstract void PrintInfo();
    }
}
