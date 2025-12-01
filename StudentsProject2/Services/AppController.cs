using StudentsProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsProject2.Services
{
    public class AppController
    {
        private readonly StudentManager studentManager = new();

        public AppController()
        {
            studentManager.StudentAdded += (student) =>
            {
                Console.WriteLine($"Student added: {student.Name}, Roll: {student.RollNumber}");
            };

        }

        public void Run()
        {
            studentManager.LoadStudentsFromFile();
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine() ?? "";
                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddStudent();
                            break;
                        case "2":
                            ViewAll();
                            break;
                        case "3":
                            SearchStudent();
                            break;
                        case "4":
                            UpdateGrade();
                            break;
                        case "5":
                            studentManager.SaveStudentsToFile();
                            Console.WriteLine("Students saved. Exiting...");
                            return;
                        case "6":
                            Console.WriteLine("Exiting without saving...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n======= STUDENT MANAGEMENT SYSTEM =======");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search Student by Roll Number");
            Console.WriteLine("4. Update Student Grade");
            Console.WriteLine("5. Save and Exit");
            Console.WriteLine("6. Exit without Saving");
            Console.Write("Enter choice: ");
        }

        private void AddStudent()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter roll number: ");

            if (!int.TryParse(Console.ReadLine(), out int roll))
                throw new Exception("Invalid roll number.");

            Console.Write("Enter grade (A-F): ");
            char g = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            var stu = new Student(name, roll, g);

            stu.GradeChanged += (s, oldG) =>

            Console.WriteLine($"Event: Grade changed for {s.Name} from {oldG} to {s.Grade}");

            studentManager.AddStudent(stu);

            Console.WriteLine("Student added!");
        }
        private void ViewAll()
        {
            Console.WriteLine("\n--- ALL STUDENTS ---");

            var students = studentManager.GetAllStudents();

            if (!students.Any())
            {
                Console.WriteLine("No students found");
                return;
            }

            foreach (var s in students)
            {
                s.PrintInfo();
            }

        }
        private void SearchStudent()
        {
            Console.Write("Enter roll number to search: ");

            if (!int.TryParse(Console.ReadLine(), out int roll))
                throw new ArgumentException("Invalid roll number.");

            var student = studentManager.FindStudent(roll);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
            }
            else
            {
                student.PrintInfo();
            }

            //Console.WriteLine($"Name:{student?.Name}, Roll: {student?.RollNumber}, Grade: {student?.Grade}");
        }
        
        private void UpdateGrade()
        {
            Console.WriteLine("Enter rolll number to update: ");

            if (!int.TryParse(Console.ReadLine(), out int roll))
                throw new ArgumentException("Invalid roll number.");

            var stu = studentManager.FindStudent(roll);

            if (stu == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter new grade (A-F): ");
            char newGrade = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            stu.UpdateGrade(newGrade);
            Console.WriteLine("Grade updated.");
        }
    }
}
