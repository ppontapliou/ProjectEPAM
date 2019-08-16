using System.Collections.Generic;
using System.Linq;

namespace Backend.Models
{
    public class Ads
    {
        public List<Ad> Ad { get; set; }
        
        public Ads()
        {

        }
        public void SortType(string type)
        {
            Ad = Ad.Where(c => c.Type.ToLower() == type.ToLower()).ToList();
        }
        public void SortCategory(string category)
        {
            Ad = Ad.Where(c => c.Category.ToLower() == category.ToLower()).ToList();
        }
    }
}