using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace StudentsProject2.Models
{
    public class Student : Person
    {
        public int RollNumber { get; set; }
        private char _grade;

        public char Grade
        {
            get => _grade;
            set
            {
                char g = char.ToUpper(value);
                if (!"ABCDEF".Any(c => c == g))
                {
                    throw new ArgumentException("Grade must be between A-F.");
                }
                _grade = g;
            }

        }
        

        public Student() { }

    
        public event Action<Student, char>? GradeChanged;

        public Student(string name, int rollNumber, char grade) : base(name)
        {
            if (rollNumber <= 0) throw new ArgumentException("Roll number must be positive.");
            this.RollNumber = rollNumber;
            this.Grade = grade;
        }

        public void UpdateGrade(char newGrade)
        {
            char old = Grade;
            this.Grade = newGrade;
            GradeChanged?.Invoke(this, old);
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}, Roll: {RollNumber}, Grade: {Grade}");
        }
    }
}
