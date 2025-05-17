using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendora.WPF.Models
{
    public static class SampleUsers
    {
        public static List<UserModel> Users { get; } = new List<UserModel>
    {
        new UserModel
        {
            Id = Guid.NewGuid().ToString(),
            Username = "admin",
            Password = "admin123",
            Name = "Ivan",
            LastName = "Petrov",
            Email = "admin@vendora.com"
        },
        new UserModel
        {
            Id = Guid.NewGuid().ToString(),
            Username = "manager01",
            Password = "manager123",
            Name = "Elena",
            LastName = "Ivanova",
            Email = "manager@vendora.com"
        }
    };
    }
    public static class AuthService
    {
        public static UserModel Authenticate(string username, string password)
        {
            return SampleUsers.Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
        public static UserModel GetByUserName(string username)
        {
            return SampleUsers.Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
