using MyLibrary;
using System;

namespace MySolNftTracker.Models
{
    [My("[User]")]
    public class AppUser
    {
        public object Id { get; set; }
        public string Username { get; set; }

        [My(MyAttribute.ActionType.Encrypted)]
        public string Password { get; set; }

        public UserRole Role { get; set; }

        public static AppUser Current
        {
            get => (AppUser)_session.Get($"current{_app.Name}User");
            set => _session.Set($"current{_app.Name}User", value);
        }
        public static bool IsAuthenticated() => _session.Exists($"current{_app.Name}User");
        public static AppUser Login(string username, string password)
        {
            var user = _attr.Get<AppUser>($"Username = {username.ToSqlString()} AND Password = '{_str.Push(password)}'");
            if (user.Count <= 0) throw new Exception("No user found");
            return user[0];
            //throw new Exception($"You don't own an item from {_app.Name} collection");
        }

        public override string ToString() => $"{Username}.";

        /*public bool ShowData { get; set; }
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string Discord { get; set; }
        public string Role { get; set; }
        public bool HasNft { get; set; }
        public bool GetNotification { get; set; }
        public static bool IsAdmin() => Current.Role.ToLower() == "admin";*/
    }

    public enum UserRole
    {
        Admin,
        User
    }
}