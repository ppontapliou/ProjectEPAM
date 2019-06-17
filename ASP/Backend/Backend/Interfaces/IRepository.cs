using Backend.Models;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IRepository
    {
        List<Ad> GetAds();
        Ad GetAd(int id);
        void DeleteAd(Ad ad);
        void PostAd(Ad value);
        void PutAd(Ad ad);
        string GetUser(Contact contact);
        void DeleteUser(int id,Contact contact);
        void PostUser(Contact contact);
        void PutUser(Contact contact);
    }
}
