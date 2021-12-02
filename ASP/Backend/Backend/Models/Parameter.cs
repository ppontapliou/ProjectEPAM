using Newtonsoft.Json;

namespace Backend.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public bool Valided
        {
            get
            {
                return Name == null ||
                Name.Length > 100 ||
                Id <= 0;
            }
        }
    }
}