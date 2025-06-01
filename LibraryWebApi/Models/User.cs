using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LibraryWebApi.Models.Abstract;
using LibraryWebApi.RoleServices;

namespace LibraryWebApi.Models
{
    public class User : BaseClass
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        //public string? Email { get; set; }

        //Programdaki Role temsili
        [NotMapped]
        public UserRole _UserRole
        {
            get => new RoleHelper().FromDb(RoleCode);
            set => RoleCode = new RoleHelper().ToDb(value);
        }

        //Db de tutulan Role temsili
        public string? RoleCode { get; set; }

    }
}
