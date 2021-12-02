using Backend.Interfaces;
using Backend.Models.DBModelsHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Backend.Models
{
    public class Repository : IRepository
    {
        ICacheHandler _cacheHandler;
        public Repository()
        {
            _cacheHandler = new CacheHandler();
        }
        #region Ads part

        public Ad GetAd(int id)
        {
            if (id <= 0)
            {
                throw new FormatException("value is not correct " + id);
            }
            Ad ad = DBHelper.GetAd(id);
            return ad;
        }

        public Ads GetPartAds(int id, int category, int state, string name, int type)
        {
            if (id < 0 || category < 0 || state < 0 || name == null)
            {
                throw new ArgumentException();
            }
            string cType = type.ToString()=="0"?"": type.ToString();

            if (category == 0 && state == 0)
            {
                return DBHelper.GetPartAds(id, name, cType);
            }
            if (category == 0)
            {
                return DBHelper.GetPartAdsState(id, state, name, cType);
            }
            if (state == 0)
            {
                return DBHelper.GetPartAdsCategory(id, category, name, cType);
            }
            return DBHelper.GetPartAds(id, category, state, name, cType);
        }

        public Ads GetAds(string type, string category)
        {
            Ads ads = DBHelper.GetAds();

            return ads;
        }

        public Ads GetUserAds(string login)
        {
            if (login.Length > 50)
            {
                throw new FormatException();
            }

            return DBHelper.GetUserAds(login);
        }

        public int AddAd(Ad ad)
        {
            if (ad == null)
            {
                throw new FormatException("Empty value");
            }
            if (!ad.Valided)
            {
                throw new FormatException("Ad object not valided");
            }
            return DBHelper.AddAd(ad);
        }

        public void ChangeAd(Ad ad, bool isAdmin)
        {
            if (ad == null)
            {
                throw new FormatException("Empty value");
            }
            if (!ad.Valided || ad.Id <= 0)
            {
                throw new FormatException("Ad object not valided");
            }
            if (isAdmin)
            {
                DBHelper.ChangeAd(ad);
                return;
            }
            DBModelHelper helper = new DBModelHelper();
            if (ad.Contact != null && ad.Contact.LoginValided)
            {
                var user = helper.Users.First(u => u.Login == ad.Contact.Login);
                if (user != null)
                {
                    if (helper.Ads.First(a => a.Id == ad.Id && a.Contact == user.Id) != null)
                    {
                        DBHelper.ChangeAd(ad);
                    }
                }
            }

        }

        public void DeleteAd(int id, string login, bool isAdmin)
        {
            if (id <= 0)
            {
                throw new FormatException("value is not correct " + id);
            }
            if (login.Length > 50)
            {
                throw new FormatException();
            }
            if (isAdmin)
            {
                DBHelper.DeleteAd(id);
            }
            else
            {
                DBHelper.DeleteAd(id, login);
            }
        }

        #endregion

        #region User part

        public Contact[] GetUsers(int id, string name)
        {
            if (id < 0 || name == null)
            {
                throw new FormatException();
            }
            return DBHelper.GetContacts(id, name);
        }

        public void CreateUser(Contact contact)
        {
            
            DBHelper.AddUser(contact);
        }

        public void ChangeUser(Contact contact, bool isAdmin)
        {
            if (isAdmin)
            {
                if (contact != null)
                {
                    DBHelper.ChangeUser(contact);
                }
                else
                {
                    throw new FormatException();
                }
            }
            else
            {
                if (contact != null)
                {
                    if (contact.Name != null || contact.Name.Length > 100)
                    {
                        DBHelper.ChangeUserName(contact.Login, contact.Name);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                else
                {
                    throw new FormatException();
                }
            }
        }
        public void DeleteUser(int id)
        {
            if (id <= 0)
            {
                throw new FormatException();
            }
            DBHelper.DeleteUser(id);
        }
        #endregion

        #region Phone part
        public void AddPhone(Contact contact, bool admin)
        {
            if (contact == null ||
                contact.Login == null ||
                contact.Login.Length > 50 ||
                contact.Phones == null ||
                contact.Phones.Count == 0 ||
                contact.Phones[0] == null ||
                contact.Phones[0].Name == null ||
                contact.Phones[0].Name.Length > 20)
            {
                throw new FormatException();
            }
            else
            {
                DBHelper.AddPhone(contact.Login, contact.Phones[0].Name);
            }
        }
        public void DeletePhone(Contact contact, bool admin)
        {
            bool complete = false;
            var phones = GetPhones(contact.Login);
            foreach (var phone in phones)
            {
                if (phone.Id == contact.Phones[0].Id)
                {
                    DBHelper.DeletePhone(contact.Phones[0].Id, contact.Login);
                    complete = true;
                    break;
                }
            }
            if (!complete)
            {
                throw new InvalidOperationException();
            }
        }
        public void ChangePhone(Contact contact, bool admin)
        {
            if (contact == null ||
                contact.Login == null ||
                contact.Login.Length > 50 ||
                contact.Phones == null ||
                contact.Phones.Count == 0 ||
                contact.Phones[0] == null ||
                contact.Phones[0].Id <= 0 ||
                contact.Phones[0].Name == null ||
                contact.Phones[0].Name.Length > 20)
            {
                throw new FormatException();
            }
            bool complete = false;
            var phones = GetPhones(contact.Login);
            foreach (var phone in phones)
            {
                if (phone.Id == contact.Phones[0].Id)
                {
                    DBHelper.ChangePhone(contact.Phones[0].Name, contact.Phones[0].Id);
                    complete = true;
                    break;
                }
            }
            if (!complete)
            {
                throw new InvalidOperationException();
            }
        }
        public List<Parameter> GetPhones(string login)
        {
            if (login == null ||
                login.Length > 50)
            {
                throw new FormatException();
            }

            return DBHelper.GetPhonesByLogin(login);
        }
        #endregion

        #region Mail part
        public void AddMail(Contact contact, bool admin)
        {
            if (contact == null ||
                contact.Login == null ||
                contact.Login.Length > 50 ||
                contact.Mails == null ||
                contact.Mails.Count == 0 ||
                contact.Mails[0] == null ||
                contact.Mails[0].Name == null ||
                contact.Mails[0].Name.Length > 50)
            {
                throw new FormatException();
            }
            else
            {
                DBHelper.AddMail(contact.Login, contact.Mails[0].Name);
            }
        }
        public void DeleteMail(Contact contact, bool admin)
        {
            bool complete = false;
            var mails = GetMails(contact.Login);
            foreach (var mail in mails)
            {
                if (mail.Id == contact.Mails[0].Id)
                {
                    DBHelper.DeleteMail(contact.Mails[0].Id);
                    complete = true;
                    break;
                }
            }
            if (!complete)
            {
                throw new InvalidOperationException();
            }
        }

        public void ChangeMail(Contact contact, bool admin)
        {
            if (contact == null ||
                contact.Login == null ||
                contact.Login.Length > 50 ||
                contact.Mails == null ||
                contact.Mails.Count == 0 ||
                contact.Mails[0] == null ||
                contact.Mails[0].Id <= 0 ||
                contact.Mails[0].Name == null ||
                contact.Mails[0].Name.Length > 50)
            {
                throw new FormatException();
            }
            bool complete = false;
            var mails = GetMails(contact.Login);
            foreach (var mail in mails)
            {
                if (mail.Id == contact.Mails[0].Id)
                {
                    DBHelper.ChangeMail(contact.Mails[0].Name, contact.Mails[0].Id);
                    complete = true;
                    break;
                }
            }
            if (!complete)
            {
                throw new InvalidOperationException();
            }
        }
        public List<Parameter> GetMails(string login)
        {
            if (login == null ||
                login.Length > 50)
            {
                throw new FormatException();
            }

            return DBHelper.GetMailsByLogin(login);
        }

        #endregion

        #region AdsData part

        public void AddCategory(Parameter parameter)
        {
            if (parameter.Name == null ||
                parameter.Name.Length > 100)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            helper.AddCategory(parameter.Name);
            _cacheHandler.RefreshCategories();
        }
        public void AddType(Parameter parameter)
        {
            if (parameter.Name == null ||
                parameter.Name.Length > 100)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            helper.AddType(parameter.Name);
            _cacheHandler.RefreshTypes();
        }
        public void AddState(Parameter parameter)
        {
            if (parameter.Name == null ||
                parameter.Name.Length > 100)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            helper.AddState(parameter.Name);
            _cacheHandler.RefreshStates();
        }

        public void ChangeCategory(Parameter category)
        {
            DBModelHelper helper = new DBModelHelper();
            if (category.Valided ||
                helper.Categories.First(c => c.Id == category.Id) == null)
            {
                throw new FormatException();
            }
            helper.ChangeCategory(category);
            _cacheHandler.RefreshCategories();
        }
        public void ChangeType(Parameter type)
        {
            DBModelHelper helper = new DBModelHelper();
            if (type.Valided ||
                helper.Types.First(c => c.Id == type.Id) == null)
            {
                throw new FormatException();
            }
            helper.ChangeType(type);
            _cacheHandler.RefreshTypes();
        }
        public void ChangeState(Parameter state)
        {
            DBModelHelper helper = new DBModelHelper();
            if (state.Valided ||
                helper.States.First(c => c.Id == state.Id) == null)
            {
                throw new FormatException();
            }
            helper.ChangeState(state);
            _cacheHandler.RefreshStates();
        }

        public void DeleteCategory(int id)
        {
            DBModelHelper helper = new DBModelHelper();
            if (id <= 0 ||
                helper.Categories.Where(c => c.Id == id) == null)
            {
                throw new FormatException();
            }
            helper.DeleteCategory(id);
            _cacheHandler.RefreshCategories();
        }
        public void DeleteType(Parameter type)
        {
            DBModelHelper helper = new DBModelHelper();
            if (type.Id <= 0 ||
                helper.Types.Where(c => c.Id == type.Id) == null)
            {
                throw new FormatException();
            }
            helper.DeleteType(type.Id);
            _cacheHandler.RefreshTypes();
        }
        public void DeleteState(int id)
        {
            DBModelHelper helper = new DBModelHelper();
            if (id <= 0 ||
                helper.States.Where(c => c.Id == id) == null)
            {
                throw new FormatException();
            }
            helper.DeleteState(id);
            _cacheHandler.RefreshStates();
        }

        public List<Parameter> GetCategories()
        {
            return _cacheHandler.GetCategories();
        }

        public List<Parameter> GetTypes()
        {
            return _cacheHandler.GetTypes();
        }

        public List<Parameter> GetStates()
        {
            return _cacheHandler.GetStates();
        }
        #endregion

        #region Image

        public void AddImage(Parameter parameter, string login, bool isAdmin)
        {
            if (isAdmin||(DBHelper.CheckUserAd(login, parameter.Id) && DBHelper.ImageCount(parameter.Id).Name != "9"))
            {
                DBHelper.AddImage(parameter);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void DeleteImages(int idAd, int id, string login, bool isAdmin)
        {
            if (isAdmin || (DBHelper.CheckUserAd(login, idAd) && DBHelper.CheckAdImage(id, idAd)))
            {
                DBHelper.DeleteImage(id, idAd);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Parameter[] GetImages(int idAd)
        {
            return DBHelper.GetImage(idAd);
        }

        #endregion
    }
}