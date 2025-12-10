using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsProject2.Models
{
    public class AuthManager
    {
        private readonly List<User> users = [];

        private static string GetFilePath()
        {
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            string data = Path.Combine(root, "Data");
            Directory.CreateDirectory(data);
            return Path.Combine(data, "users.xml");
        }

        public void LoadUsers()
        {
            string filePath = GetFilePath();
            if (!File.Exists(filePath))
            {
                SaveUsers();
                return;
            }

            var serializer = new XmlSerializer(typeof(List<User>));
            using var fs = new FileStream(filePath, FileMode.Open);

            if (fs.Length == 0) return;

            if (serializer.Deserialize(fs) is List<User> loaded)
            {
                users.Clear();
                users.AddRange(loaded);
            }
        }

        public void SaveUsers()
        {
            string filePath = GetFilePath();
            var serializer = new XmlSerializer(typeof(List<User>));
            using var fs = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(fs, users);
        }

        public bool Register(string username, string password)
        {
            if (users.Any(u => u.Username == username))
                return false;

            users.Add(new User(username, password));
            SaveUsers();
            return true;
        }

        public User? Login(string username, string password)
        {
            return users.FirstOrDefault(u => u.Username == username && u.ValidatePassword(password));
        }
    }
}
