using System.Text.Json.Serialization;

namespace LibraryWebApi.Models.Abstract
{
    public abstract class BaseClass
    {
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; } = default(DateTime?);
        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }
    }
}
