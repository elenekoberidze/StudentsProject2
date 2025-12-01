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
            const string fileName = "students.xml";
            const string targetDirectory = "Data";
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentPath);
            while (directory != null && directory.Name != "bin")
            {
                directory = directory.Parent;
            }
            DirectoryInfo projectRoot = (directory?.Parent) ?? throw new DirectoryNotFoundException("Could not find the project root directory by searching for 'bin'.");
            string dataPath = Path.Combine(projectRoot.FullName, targetDirectory);
            Directory.CreateDirectory(dataPath);
            return Path.Combine(dataPath, fileName);
        }
        public void SaveStudentsToFile()
        {
            string filePath = GetFilePath();

            XmlRootAttribute root = new("ArrayOfStudents");
            XmlSerializer serializer = new(typeof(List<Student>), root);

            try
            {
                using FileStream fs = new(filePath, FileMode.Create);
                serializer.Serialize(fs, students);
                Console.WriteLine($"Successfully saved {students.Count} students to:\n{filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }

        }

        public void LoadStudentsFromFile()
        {
            string filePath = GetFilePath();

            if (!File.Exists(filePath))
            {
                //Console.WriteLine("Data file 'students.xml' not found. Starting with an empty list.");
                return;
            }

            XmlRootAttribute root = new("ArrayOfStudents");
            XmlSerializer serializer = new(typeof(List<Student>), root);

            try
            {
                using FileStream fs = new(filePath, FileMode.Open);

            
                if (fs.Length == 0)
                {
                    Console.WriteLine("Data file 'students.xml' exists but is empty. Starting with an empty list.");
                    return;
                }

                if (serializer.Deserialize(fs) is List<Student> loaded)
                {
                    students.Clear();
                    students.AddRange(loaded);
                    Console.WriteLine($"Successfully loaded {students.Count} students from file."); 
                }
            }
            //catch (InvalidOperationException ex)
            //{
               
            //    Console.WriteLine($"Error deserializing XML data (Corrupted File).");
            //    Console.WriteLine($"Details: {ex.InnerException?.Message ?? ex.Message}");
            //}
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data. The file might be corrupted.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }
    }

}
    
