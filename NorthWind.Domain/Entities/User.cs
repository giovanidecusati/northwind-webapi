using NorthWind.Shared.Entities;
using NorthWind.Shared.Validators;
using System.Text;

namespace NorthWind.Domain.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        protected User() { }

        public User(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = EncryptPassword(password);

            new ValidationContract<User>(this)
                .IsRequired(p => p.FirstName)
                .HasMaxLenght(p => p.FirstName, 256)
                .IsRequired(p => p.LastName)
                .HasMaxLenght(p => p.LastName, 256)
                .IsRequired(p => p.Email)
                .HasMaxLenght(p => p.Email, 256)
                .IsRequired(p => p.Password)
                .AreEquals(p => p.Password, EncryptPassword(confirmPassword));
        }

        public bool Authenticate(string email, string password)
        {
            if (Email == email && Password == EncryptPassword(password))
                return true;

            AddNotification("User", "Invalid user name or password.");
            return false;
        }

        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass))
                return "";

            var password = (pass += "|6bcd5052-20bf-4a86-8d87-bcaf49f89522" + Email);
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
