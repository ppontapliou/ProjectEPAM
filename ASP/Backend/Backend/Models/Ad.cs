using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{

    public class Ad
    {
        
        public int Id { get; set; }
        public string NameAd { get; set; }
        public string Title { get; set; }
        public DateTime DateCreation { get; set; }
        public string Picture { get; set; }
        public string Adress { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public Contact Contact { get; set; }

        public Ad()
        {

        }
        public override string ToString()
        {
            return $"\'{NameAd}\', \'{Title}\', \'{DateCreation}\', \'{Picture}\', {Category}, \'{Adress}\', {Type}, {State}, \'{Contact.Login}\', \'{Contact.Password}\'";
        }
        public string ToStringWithId()
        {
            return $"\'{NameAd}\', \'{Title}\', \'{DateCreation}\', \'{Picture}\', {Category}, \'{Adress}\', {Type}, {State}, \'{Contact.Login}\', \'{Contact.Password}\', {Id}";
        }
    }
}