using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Categories
    {
        public List<Category> ListCategories { get; set; }
        public Categories()
        {
            ListCategories = new List<Category>();
        }
        public void AddCategory(string[] lines)
        {
            if (lines == null) throw new ArgumentNullException();
            if (lines.Length != 2) throw new ArgumentOutOfRangeException("Need two value in range");
            Category category = new Category() { Id = Convert.ToInt32(lines[0]), NameCategory = lines[1] };

            ListCategories.Add(category);
        }
    }
}