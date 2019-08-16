using Backend.Models.DBModelsHelper;
using System;
using System.Linq;

namespace Backend.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
            Contact = new Contact();
        }

        public int ContactId
        {
            set
            {
                this.Contact.Id = value;
                this.Contact.Phones = DBHelper.GetPhones(value);
                this.Contact.Mails = DBHelper.GetMails(value);
            }
        }
        public string ContactName { set => this.Contact.Name = value; }

        public bool Valided
        {
            get
            {
                DBModelHelper helper = new DBModelHelper();
                if (Name == null ||
                    Name.Length > 100 ||
                    Title == null ||
                    Title.Length > 3000 ||
                    Picture == null ||
                    !Uri.IsWellFormedUriString(Picture, UriKind.RelativeOrAbsolute) ||
                    Adress == null ||
                    Adress.Length > 150 ||
                    Category == null ||
                    helper.Categories.First(t => t.Category == this.Category) == null ||
                    Type == null ||
                    helper.Types.First(t => t.Type == this.Type) == null ||
                    State == null ||
                    helper.States.First(s => s.State == this.State) == null)
                {
                    return false;
                }
                return true;
            }
        }
        public int IdCategory()
        {
            DBModelHelper helper = new DBModelHelper();
            return helper.Categories.First(t => t.Category == this.Category).Id;
        }
        public int IdType()
        {
            DBModelHelper helper = new DBModelHelper();
            return helper.Types.First(t => t.Type == this.Type).Id;
        }
        public int IdState()
        {
            DBModelHelper helper = new DBModelHelper();
            return helper.States.First(s => s.State == this.State).Id;
        }
    }
}