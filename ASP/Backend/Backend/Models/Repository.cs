using Backend.Interfaces;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Repository : IRepository
    {
       

        public Ad GetAd(int id)
        {
            return new Ads(DBHelper.GetAds("exec GetAd " + id)).Ad[0];
        }

        public List<Ad> GetAds()
        {
            return new Ads(DBHelper.GetAds("exec GetAds")).Ad;
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
            return DBHelper.GetContactsInfo("exec Authentication " + contact.LoginAndPassword);
        }

        public void DeleteUser(int id,Contact contact)
        {
           
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