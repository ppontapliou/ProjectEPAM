using System.Collections.Generic;
using System.Linq;
using Backend.Models.DBModelsHelper;
using Newtonsoft.Json;
using NLog;


namespace Backend.Models
{

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Parameter> Phones { get; set; }
        public List<Parameter> Mails { get; set; }
        [JsonProperty("Login")]
        public string Login { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }

        public string Role { get; set; }
        [JsonIgnore]
        public string LogDate { private get; set; }
        [JsonIgnore]
        public int IdRole { get => Role != null ? new DBModelHelper().Statuses.First(s => s.Status == Role).Id : -1; }
        [JsonIgnore]
        public int IdUser { get => Login != null ? new DBModelHelper().Users.First(u => u.Login == Login).Id : -1; }
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public Contact()
        {
            LogDate = "Forget give name";
        }


        public bool Validated(bool withIdParameter)
        {
            DBModelHelper helper = new DBModelHelper();
            bool relult = true;
            if (withIdParameter ||
                Id <= 0)
            {
                relult = false;
                Logger.Info("Bad id value " + LogDate);
            }
            if (Name == null || Name.Length > 100)
            {
                relult = false;
                Logger.Info("Name is empty. " + LogDate);
            }
            if (Password == null || Password.Length > 30)
            {
                relult = false;
            }
            if (Login == null ||
                Login.Length > 50 ||
                helper.Users.First(u => u.Login == Login) != null)
            {
                relult = false;
            }
            if (Role == null ||
                helper.Statuses.First(s => s.Status == Role) == null)
            {
                relult = false;
            }
            if (Mails == null ||
                Mails.Count == 0)
            {
                relult = false;
            }
            if (Mails[0] == null ||
                Mails[0].Name == null ||
                Mails[0].Name.Length > 50)
            {
                relult = false;
            }
            return relult;
        }
        [JsonIgnore]
        public bool LoginValided
        {
            get
            {
                DBModelHelper helper = new DBModelHelper();
                bool result = true;
                if (Login == null)
                {
                    result = false;
                }
                if (Login == null ||
                Login.Length > 50 ||
                helper.Users.First(u => u.Login == Login) == null)
                {
                    result = false;
                }
                return result;
            }
        }

    }
}