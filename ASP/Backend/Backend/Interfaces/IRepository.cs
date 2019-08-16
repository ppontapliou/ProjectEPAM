using Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Interfaces
{
    public interface IRepository
    {
        Ads GetAds(string type, string category);
        Ads GetUserAds(string login);
        Ad GetAd(int id);
        void DeleteAd(int Id, string login, bool isAdmin);
        void AddAd(Ad ad);
        void ChangeAd(Ad ad, bool isAdmin);

        void CreateUser(Contact contact);
        void ChangeUser(Contact contact, bool admin);
        void DeleteUser(int id);

        void AddPhone(Contact contact, bool admin);
        void DeletePhone(Contact contact, bool admin);
        void ChangePhone(Contact contact, bool admin);
        List<Parameter> GetPhones(string name);

        void AddMail(Contact contact, bool admin);
        void DeleteMail(Contact contact, bool admin);
        void ChangeMail(Contact contact, bool admin);
        List<Parameter> GetMails(string name);

        void AddCategory(Parameter parameter);
        void AddType(Parameter parameter);
        void AddState(Parameter parameter);

        void ChangeCategory(Parameter parameter);
        void ChangeType(Parameter parameter);
        void ChangeState(Parameter parameter);

        void DeleteCategory(Parameter parameter);
        void DeleteType(Parameter parameter);
        void DeleteState(Parameter parameter);

        List<Parameter> GetCategories();
        List<Parameter> GetTypes();
        List<Parameter> GetStates();
    }
}
