                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class DBHelper
    {

        static readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=EPAM_Project;Integrated Security=True";

        #region Ad region
        public static Ads GetAds()
        {
            string sqlExpression = "EXEC GetAds";

            string rezult = ReturnData(sqlExpression);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }
        public static Ad GetAd(int id)
        {
            string sqlExpression = "EXEC GetAd @id";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads.Ad[0];
        }
        public static Ads GetUserAds(string login)
        {
            string sqlExpression = "EXEC GetUserAds @Login";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", login)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }

        public static Ads GetPartAds(int id, string name, string type)
        {
            string sqlExpression = "EXEC GetPartAds @Id, @Name, @Type";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", name),
                new SqlParameter("Type", type)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }

        public static Ads GetPartAdsCategory(int id, int category, string name, string type)
        {
            string sqlExpression = "EXEC GetPartAdsCategory @Id, @Name, @Category, @Type";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("Category", category),
                new SqlParameter("@Name", name),
                new SqlParameter("Type", type)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }

        public static Ads GetPartAdsState(int id, int state, string name, string type)
        {

            string sqlExpression = "EXEC GetPartAdsState @Id, @Name, @State, @Type";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("State", state),
                new SqlParameter("@Name", name),
                new SqlParameter("Type", type)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }

        public static Ads GetPartAds(int id, int category, int state, string name, string type)
        {
            string sqlExpression = "EXEC GetPartAdsMulty @Id, @Name, @Category, @State, @Type";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Category", category),
                new SqlParameter("State", state),
                new SqlParameter("@Name", name),
                new SqlParameter("Type", type)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }

        public static int AddAd(Ad ad)
        {
            string sqlExpression = "EXECUTE AddAd @Ad, @Title, @DataCreation, @Picture, @Category, @Adress, @Type, @State, @Login";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Ad", ad.Name),
                new SqlParameter("@Title", ad.Title),
                new SqlParameter("@DataCreation", DateTime.Now),
                new SqlParameter("@Picture", ad.Picture),
                new SqlParameter("@Category", ad.IdCategory()),
                new SqlParameter("@Type", ad.IdType()),
                new SqlParameter("@State", ad.IdState()),
                new SqlParameter("@Adress", ad.Adress),
                new SqlParameter("@Login", ad.Contact.Login),
            };

            //SendRequest(sqlExpression, sqlParameters);
            string rezult = ReturnData(sqlExpression, sqlParameters);
            return JsonConvert.DeserializeObject<Parameter[]>(rezult)[0].Id;
        }
        public static void ChangeAd(Ad ad)
        {
            string sqlExpression = "EXECUTE ChangeAd @Id, @Ad, @Title, @Picture, @Category, @Adress, @Type, @State, @Login";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", ad.Id),
                new SqlParameter("@Ad", ad.Name),
                new SqlParameter("@Title", ad.Title),
                new SqlParameter("@Picture", ad.Picture),
                new SqlParameter("@Category", ad.IdCategory()),
                new SqlParameter("@Type", ad.IdType()),
                new SqlParameter("@State", ad.IdState()),
                new SqlParameter("@Adress", ad.Adress),
                new SqlParameter("@Login", ad.Contact.Login),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeleteAd(int id, string login)
        {
            string sqlExpression = "EXECUTE DeleteAd @Login, @IdAd";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdAd", id),
                new SqlParameter("@Login", login),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeleteAd(int id)
        {
            string sqlExpression = "EXECUTE DeleteAdById @Id";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        #endregion

        #region User region
        public static Contact[] GetContacts(int id, string name)
        {
            string sqlExpression = "EXECUTE GetPartUsers @Id, @Name";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name",name),
            };
            string rezult = ReturnData(sqlExpression, sqlParameters);
            Contact[] contacts = JsonConvert.DeserializeObject<Contact[]>(rezult);
            return contacts;
        }

        public static void AddUser(Contact contact)
        {
            string sqlExpression = "EXECUTE AddContact @Login, @Password, @Name, @Role, @Mail";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", contact.Login),
                new SqlParameter("@Password", contact.Password),
                new SqlParameter("@Name", contact.Name),
                new SqlParameter("@Role", contact.IdRole),
                new SqlParameter("@Mail", contact.Mails[0].Name),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void ChangeUser(Contact contact)
        {
            string sqlExpression = "EXECUTE ChangeUser @Id, @Login, @Password, @Name, @Role";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id",contact.Id),
                new SqlParameter("@Login", contact.Login),
                new SqlParameter("@Password", contact.Password),
                new SqlParameter("@Name", contact.Name),
                new SqlParameter("@Role", contact.IdRole),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void ChangeUserName(string login, string userName)
        {
            string sqlExpression = "EXECUTE ChangeUser  @Login, @Name";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", login),
                new SqlParameter("@Name", userName),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeleteUser(int idContact)
        {
            string sqlExpression = "EXECUTE DeleteUser @Id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id",idContact),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        #endregion

        #region Phone region
        public static void AddPhone(string login, string phone)
        {
            string sqlExpression = "EXECUTE AddPhone @Login, @Phone";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login",login),
                new SqlParameter("@Phone",phone),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void ChangePhone(string phone, int idPhone)
        {
            string sqlExpression = "EXECUTE ChangePhone @IdPhone, @Phone";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdPhone",idPhone),
                new SqlParameter("@Phone",phone),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeletePhone(int idPhone, string login)
        {
            string sqlExpression = "EXECUTE DeletePhone @IdPhone, @Login";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdPhone",idPhone),
                new SqlParameter("@Login",login),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static List<Parameter> GetPhones(int id)
        {
            string sqlExpression = "EXEC GetPhones @Id";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);

            return JsonConvert.DeserializeObject<List<Parameter>>(rezult);
        }

        public static List<Parameter> GetPhonesByLogin(string login)
        {
            string sqlExpression = "EXEC GetPhonesByLogin @Login";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", login)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);

            return JsonConvert.DeserializeObject<List<Parameter>>(rezult);
        }
        #endregion

        #region Mail region
        public static void AddMail(string login, string mail)
        {
            string sqlExpression = "EXECUTE AddMail @Login, @Mail";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login",login),
                new SqlParameter("@Mail",mail),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void ChangeMail(string mail, int idMail)
        {
            string sqlExpression = "EXECUTE ChangeMail @IdMail, @Mail";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdMail",idMail),
                new SqlParameter("@Mail",mail),
            };
            SendRequest(sqlExpression, sqlParameters);
        }

        public static void DeleteMail(int idMail)
        {
            string sqlExpression = "EXECUTE DeleteMail @IdMail";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdMail",idMail),
            };
            SendRequest(sqlExpression, sqlParameters);
        }

        public static List<Parameter> GetMails(int id)
        {
            string sqlExpression = "EXEC GetMails @Id";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);

            return JsonConvert.DeserializeObject<List<Parameter>>(rezult);
        }

        public static List<Parameter> GetMailsByLogin(string login)
        {
            string sqlExpression = "EXEC GetMailsByLogin @Login";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", login)
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);

            return JsonConvert.DeserializeObject<List<Parameter>>(rezult);
        }
        #endregion

        #region AdsData

        public static List<Parameter> GetCategories()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> GetTypes()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> GetStates()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> InsertCategories()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> InsertTypes()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> InsertStates()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> UpdateCategories()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> UpdateTypes()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> UpdateStates()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> DelateCategories()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> DelateTypes()
        {
            throw new NotImplementedException();
        }

        public static List<Parameter> DelateStates()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Images

        public static bool CheckUserAd(string login, int idAd)
        {
            string sqlExpression = "EXECUTE CheckUserAd @Login, @IdAd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", login),
                new SqlParameter("@IdAd", idAd),
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            return JsonConvert.DeserializeObject<Parameter[]>(rezult)[0].Name == "1";
        }

        public static bool CheckAdImage(int id, int idAd)
        {
            string sqlExpression = "EXECUTE CheckUserAd @Id, @IdAd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@IdAd", idAd),
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            return JsonConvert.DeserializeObject<Parameter[]>(rezult)[0].Name == "1";
        }

        public static Parameter ImageCount(int idAd)
        {
            string sqlExpression = "EXECUTE CountImage @IdAd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdAd", idAd),
            };

            string rezult = ReturnData(sqlExpression, sqlParameters);
            return JsonConvert.DeserializeObject<Parameter[]>(rezult)[0];
        }

        public static void AddImage(Parameter parameter)
        {
            string sqlExpression = "EXECUTE AddAdImage @IdAd, @Path";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdAd", parameter.Id),
                new SqlParameter("@Path", parameter.Name),
            };
            SendRequest(sqlExpression, sqlParameters);
        }

        public static void DeleteImage(int id, int idAd)
        {
            string sqlExpression = "EXECUTE DeleteImage @Id,@IdAd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@IdAd", idAd),
            };
            SendRequest(sqlExpression, sqlParameters);
        }

        public static Parameter[] GetImage(int idAd)
        {
            string sqlExpression = "EXECUTE GetImage @IdAd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdAd", idAd),
            };
            string rezult = ReturnData(sqlExpression, sqlParameters);
            return JsonConvert.DeserializeObject<Parameter[]>(rezult);
        }

        #endregion
        /*_________________________________________________________________________________________________________________________________________________________________________*/
        public static string ReturnData(string sqlExpression, List<SqlParameter> sqlParameters = null)
        {
            string rezult = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                if (sqlParameters != null)
                    foreach (var param in sqlParameters)
                    {
                        command.Parameters.Add(param);
                    }
                SqlDataReader reader = command.ExecuteReader();
                rezult = SQLDataToJson(reader);
                reader.Close();
            }
            return rezult;
        }
        private static void SendRequest(string sqlExpression, List<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                foreach (var param in sqlParameters)
                {
                    command.Parameters.Add(param);
                }
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
        private static string SQLDataToJson(SqlDataReader dataReader)
        {
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            string JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          