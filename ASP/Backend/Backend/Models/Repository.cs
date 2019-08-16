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
            //IUnityContainer ServiceLocator = new UnityContainer();
            //ServiceLocator.RegisterType(typeof(ICacheHandler), typeof(CacheHandler));
            //_cacheHandler = ServiceLocator.Resolve<ICacheHandler>();
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
        public Ads GetAds(string type, string category)
        {
            DBModelHelper helper = new DBModelHelper();
            Ads ads = DBHelper.GetAds();
            if (type != null)
            {
                if (helper.Types.First(t => t.Type == type) == null)
                {
                    throw new FormatException("not corrected type " + type);
                }
                ads.SortType(type);
            }
            if (category != null)
            {
                if (helper.Categories.First(t => t.Category == category) == null)
                {
                    throw new FormatException("not corrected category " + category);
                }
                ads.SortCategory(category);
            }
            return ads;
        }
        public Ads GetUserAds(string login)
        {
            DBHelper helper = new DBHelper();
            if (login.Length > 50)
            {
                throw new FormatException();
            }
            return DBHelper.GetUserAds(login);
        }
        public void AddAd(Ad ad)
        {
            if (ad == null)
            {
                throw new FormatException("Empty value");
            }
            if (!ad.Valided)
            {
                throw new FormatException("Ad object not valided");
            }
            DBHelper.AddAd(ad);
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
                if (ad.Contact != null && ad.Contact.LoginValided)
                {
                    DBHelper.ChangeAd(ad);
                }
            }
            else
            {
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
        public void CreateUser(Contact contact)
        {
            if (contact == null ||
                !contact.Validated(false))
            {
                DBHelper.AddUser(contact);
            }
            else
            {
                throw new FormatException();
            }
        }
        public void ChangeUser(Contact contact, bool isAdmin)
        {
            if (isAdmin)
            {
                if (contact == null ||
                    !contact.Validated(true))
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
            if (contact.Id <= 0)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            int idUser = helper.Users.First(u => u.Login == contact.Login).Id;
            bool hasNumber = helper.ContactsPhones.First(c => c.Phone == contact.Phones[0].Id && c.Contact == idUser) != null;
            if (hasNumber)
            {
                DBHelper.DeletePhone(contact.Phones[0].Id);
            }
        }
        public void ChangePhone(Contact contact, bool admin)
        {
            DBModelHelper helper = new DBModelHelper();
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
            int idUser = helper.Users.First(u => u.Login == contact.Login).Id;
            bool hasNumber = helper.ContactsPhones.First(c => c.Phone == contact.Phones[0].Id && c.Contact == idUser) != null;
            if (hasNumber)
            {
                DBHelper.ChangePhone( contact.Phones[0].Name, contact.Phones[0].Id);
            }
        }
        public List<Parameter> GetPhones(string login)
        {
            if (login == null||
                login.Length>50)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            int idUser = helper.Users.First(u => u.Login == login).Id;
            return DBHelper.GetPhones(idUser);
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
            if (contact.Id <= 0)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            int idUser = helper.Users.First(u => u.Login == contact.Login).Id;
            bool hasNumber = helper.ContactsEmails.First(c => c.Email == contact.Mails[0].Id && c.Contact == idUser) != null;
            if (hasNumber)
            {
                DBHelper.DeleteMail( contact.Mails[0].Id);
            }
        }
        public void ChangeMail(Contact contact, bool admin)
        {
            DBModelHelper helper = new DBModelHelper();
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
            int idUser = helper.Users.First(u => u.Login == contact.Login).Id;
            bool hasNumber = helper.ContactsEmails.First(c => c.Email == contact.Mails[0].Id && c.Contact == idUser) != null;
            if (hasNumber)
            {
                DBHelper.ChangeMail(contact.Login, contact.Mails[0].Name, contact.Mails[0].Id);
            }
        }
        public List<Parameter> GetMails(string login)
        {
            if (login == null ||
                login.Length > 50)
            {
                throw new FormatException();
            }
            DBModelHelper helper = new DBModelHelper();
            int idUser = helper.Users.First(u => u.Login == login).Id;
            return DBHelper.GetMails(idUser);
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
            _cacheHandler.RefreshTypes();
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

        public void DeleteCategory(Parameter category)
        {
            DBModelHelper helper = new DBModelHelper();
            if (category.Id <= 0 ||
                helper.Categories.Where(c => c.Id == category.Id) == null)
            {
                throw new FormatException();
            }
            helper.DeleteCategory(category.Id);
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
        public void DeleteState(Parameter state)
        {
            DBModelHelper helper = new DBModelHelper();
            if (state.Id <= 0 ||
                helper.States.Where(c => c.Id == state.Id) == null)
            {
                throw new FormatException();
            }
            helper.DeleteState(state.Id);
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
    }
}