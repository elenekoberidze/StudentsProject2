# 🎓 Student Management System
 
A **C# console application** for managing students — add, view, search, and update student records — with user authentication and persistent XML storage.
 
<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white">
  <img alt="C%23" src="https://img.shields.io/badge/C%23-console--app-239120?logo=csharp&logoColor=white">
  <img alt="License" src="https://img.shields.io/badge/license-MIT-green">
</p>
---
 
## 📖 Overview
 
**StudentsProject2** is a menu-driven console app for managing a small student roster. Users register or log in, then can add students, list all students, search by roll number, and update grades. All data — both user accounts and student records — is persisted to disk as XML, so it survives between runs.
 
It's a compact example of layered console-app design in C#: models, a manager/service layer, an app controller for the menu flow, and simple file-based persistence via `XmlSerializer`.
 
---
 
## ✨ Features
 
- 🔐 **Login & Registration** — simple username/password auth before accessing the system
- ➕ **Add Student** — name, roll number, and a letter grade (A–F)
- 📋 **View All Students** — list every student currently loaded
- 🔍 **Search by Roll Number** — quickly find a specific student
- ✏️ **Update Grade** — change a student's grade, with a `GradeChanged` event fired on update
- 💾 **XML Persistence** — students and users are saved/loaded from `Data/students.xml` and `Data/users.xml`
- 📡 **Event-driven Notifications** — `StudentManager.StudentAdded` and `Student.GradeChanged` events log actions to the console
- ✅ **Input Validation** — roll numbers must be positive and unique; grades must be A–F; passwords capped at 10 characters
---
 
## 🧱 Tech Stack
 
| Layer       | Technology                  |
|--------------|--------------------------------|
| Runtime       | .NET 8 (Console App)             |
| Language      | C#                                |
| Persistence   | XML (`System.Xml.Serialization`)   |
 
---
 
## 📁 Project Structure
 
```
StudentsProject2/
├── Data/
│   ├── students.xml       # Persisted student records
│   └── users.xml            # Persisted user accounts
├── Models/
│   ├── Person.cs             # Abstract base class (Name, PrintInfo)
│   ├── Student.cs             # Student entity (RollNumber, Grade, GradeChanged event)
│   ├── User.cs                 # User account (Username, Password, validation)
│   └── AuthManager.cs           # Loads/saves users, handles login & registration
├── Services/
│   ├── StudentManager.cs         # CRUD + XML persistence for students
│   └── AppController.cs           # Console menu flow / app entry logic
├── Program.cs
└── StudentsProject2.csproj
```
 
---
 
## 🚀 Getting Started
 
### Prerequisites
 
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
### Run the app
 
```bash
git clone https://github.com/<your-username>/StudentsProject2.git
cd StudentsProject2/StudentsProject2
dotnet run
```
 
### How to use it
 
1. At the **login menu**, choose `1` to log in with an existing account, or `2` to register a new one.
2. Once logged in, use the **main menu**:
```
   1. Add Student
   2. View All Students
   3. Search Student by Roll Number
   4. Update Student Grade
   5. Save and Exit
   6. Exit without Saving
```
3. Choose `5` to persist your changes to `Data/students.xml` before exiting, or `6` to discard them.
---
 
## 🗃️ Data Model
 
- **Person** *(abstract)* → base class with a `Name` and an abstract `PrintInfo()` method
- **Student** *(extends Person)* → adds `RollNumber` (must be positive & unique) and `Grade` (restricted to `A`–`F`); raises a `GradeChanged` event whenever the grade is updated
- **User** → `Username` + `Password` (max 10 characters), with basic credential validation
---
 
## ⚠️ Known Limitations
 
This is a learning/practice project, so a few things are worth knowing before using it for anything real:
 
- Passwords are stored largely **in plain text** in `users.xml` (a couple of entries appear hashed, most aren't) — this is **not secure** and shouldn't be used as-is for real credentials.
- Data is stored in flat XML files rather than a database, so it won't scale past a small number of records.
- There's no encryption, salting, or proper auth token handling — authentication is intentionally minimal for demonstration purposes.
---
 
## 🛠️ Possible Improvements
 
- Hash and salt passwords properly (e.g. with `BCrypt` or `PBKDF2`) instead of storing them in plain text
- Move persistence to a real database (SQLite, SQL Server, etc.)
- Add editing/deleting of students, not just adding and grade updates
- Add unit tests for `StudentManager` and `AuthManager`
---
 
## 🤝 Contributing
 
Contributions are welcome!
 
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
---
 
## 📄 License
 
This project is available under the MIT License. Feel free to use it for learning or as a starting point for your own projects.
 
---
 
<p align="center">Made with ❤️ using C#</p>
 
