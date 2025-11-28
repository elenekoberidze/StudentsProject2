using StudentsProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsProject2.Services
{
    public class StudentManager
    {
        private readonly List<Student> students = [];
        public event Action<Student>? StudentAdded;
        public void AddStudent(Student student)
        {
            if (students.Any(s => s.RollNumber == student.RollNumber))
            {
                throw new ArgumentException($"Student with roll number {student.RollNumber} already exists.");
            }
            students.Add(student);
            StudentAdded?.Invoke(student);
        }

        public IEnumerable<Student> GetAllStudents() => students;

        public Student? FindStudent(int rollNumber)
        {
            return students.FirstOrDefault(s => s.RollNumber == rollNumber);
        }

        public void UpdateStudentGrade(int rollNumber, char newGrade)
        {
            var student = FindStudent(rollNumber) ?? throw new ArgumentException($"Student with roll number {rollNumber} not found.");
            student.UpdateGrade(newGrade);
        }


        public void SaveStudentsToFile()
        {
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "students.xml");

            XmlSerializer serializer = new(typeof(List<Student>));

            using FileStream fs = new(filePath, FileMode.Create);
            serializer.Serialize(fs, students);

        }

        public void LoadStudentsFromFile()
        {
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data", "students.xml");

            if (!File.Exists(filePath))
            {
                return;
            }

            XmlSerializer serializer = new(typeof(List<Student>));

            using FileStream fs = new(filePath, FileMode.Open);

            if (serializer.Deserialize(fs) is List<Student> loaded)
            {
                students.Clear();
                foreach (var student in loaded)
                {
                    students.Add(student);
                }
            }

        }
    }
}
