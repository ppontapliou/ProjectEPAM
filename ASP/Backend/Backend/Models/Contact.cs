using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Phones { get; set; }
        public List<string> Mails { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Contact() { }

        public void AddMails(params string[] mails)
        {
            if (Mails == null) Mails = new List<string>();
            if (mails.Length > 0)
            {
                Mails.AddRange(mails);
            }
        }
        public void AddPhones(string phones)
        {
            if (Phones == null) Phones = new List<string>();
            if (phones.Length > 0)
            {
                Phones.Add(phones);
            }
        }        
    }
}