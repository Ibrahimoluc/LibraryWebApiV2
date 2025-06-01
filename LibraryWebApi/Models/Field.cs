using System.Text.Json.Serialization;
using LibraryWebApi.Models.Abstract;
using Microsoft.AspNetCore.HttpLogging;

namespace LibraryWebApi.Models
{
    public class Field : BaseClass
    {
        public int Id { get; set; } 
        public string? Name { get; set; }

    }
}
