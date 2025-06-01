using System.Text.Json.Serialization;
using LibraryWebApi.Models.Abstract;

namespace LibraryWebApi.Models
{
    public class Author : BaseClass
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Nation {  get; set; }
    }
}
