using System.Drawing;

namespace LibraryWebApi.RoleServices
{
    public static class Roles
    {
        public const string Admin = "admin";
        public const string User = "user";
    }

    public class UserRole
    {
        private readonly static string[] AllowedRoles = { Roles.Admin, Roles.User };

        public string? _role; 
        public string? Role
        {
            get => _role;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (!AllowedRoles.Contains(value)) throw new ArgumentException("Geçersiz Role");
                _role = value;
            }
        }

        public override string ToString()
        {
            return _role;
        }
    }
}
