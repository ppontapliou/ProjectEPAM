using Backend.Interfaces;
using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Repository : IRepository
    {
        public Ad GetAd(int id)
        {
            return DBHelper.GetAd(id);//return ad from request
        }

        public List<Ad> GetAds()
        {
            return DBHelper.GetAds();//return property with all ads
        }

        public void PostAd(Ad ad)
        {
            DBHelper.AddAd(ad);
        }

        public void PutAd(Ad ad)
        {
            DBHelper.UpdateAd(ad);
        }

        public void DeleteAd(Ad ad)
        {
            DBHelper.DeleteAd(ad);           
        }

        public string GetUser(Contact contact)
        {            
            
            return DBHelper.GetContactsInfo("exec Authentication @Login, @Password",contact);
        }

        public void DeleteUser(int id,Contact contact)
        {
            DBHelper.DeleteUser(id,contact);
        }

        public void PostUser(Contact contact)
        {
            DBHelper.CerateContact(contact);
        }

        public void PutUser(Contact contact)
        {
            DBHelper.ChangeUserName(contact);
        }
    }
}