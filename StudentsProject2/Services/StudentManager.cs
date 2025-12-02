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

        private static string GetFilePath()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            string dataPath = Path.Combine(projectRoot, "Data");
            Directory.CreateDirectory(dataPath);
            string filePath = Path.Combine(dataPath, "students.xml");
            if (!File.Exists(filePath))
            {
                using var fs = File.Create(filePath);
                fs.Close();


                var serializer = new XmlSerializer(typeof(List<Student>));
                using var sw = new StreamWriter(filePath);
                serializer.Serialize(sw, new List<Student>());
            }

            return filePath;
        }
        public void SaveStudentsToFile()
        {
            string filePath = GetFilePath();
            var serializer = new XmlSerializer(typeof(List<Student>));

            using var fs = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(fs, students);

            Console.WriteLine($"Saved {students.Count} students to {filePath}");

        }

        public void LoadStudentsFromFile()
        {
            string filePath = GetFilePath();
            if (!File.Exists(filePath)) return;

            var serializer = new XmlSerializer(typeof(List<Student>));

            try
            {
                using var fs = new FileStream(filePath, FileMode.Open);
                if (fs.Length == 0) return;

                if (serializer.Deserialize(fs) is List<Student> loaded)
                {
                    students.Clear();
                    students.AddRange(loaded);
                    Console.WriteLine($"Loaded {students.Count} students from {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

    }
}
    
