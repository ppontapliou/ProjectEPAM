using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronimic { get; set; }
        public Person()
        {
            Name = "Name";
            Surname = "Surname";
            Patronimic = "Patronimic";
        }
    }
}