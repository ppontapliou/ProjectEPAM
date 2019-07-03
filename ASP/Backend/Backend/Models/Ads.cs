using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Ads
    {
        public List<Ad> Ad { get; set; }
        public Ads(List<string[]> arrs)
        {
            Ad = new List<Ad>();
            foreach (var arr in arrs)
            {
                Ad ad = new Ad
                {
                    Id = Convert.ToInt32(arr[0]),
                    NameAd = arr[1],
                    Title = arr[2],
                    DateCreation = DateTime.Parse(arr[3]),
                    Picture = arr[4],
                    Category = arr[5],
                    Contact = new Contact() { Name = arr[6], Id = Convert.ToInt32(arr[10]) },
                    Adress = arr[7],
                    Type = arr[8],
                    State = arr[9]
                };
                DBHelper.GetContactsInfo("exec GetPhones " + ad.Contact.Id, x => ad.Contact.AddPhones(x));
                DBHelper.GetContactsInfo("exec GetMails " + ad.Contact.Id, x => ad.Contact.AddMails(x));
                this.Ad.Add(ad);
            }
        }
    }
}