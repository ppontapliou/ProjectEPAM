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

            string rezult = ReturnData(sqlExpression,sqlParameters);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads.Ad[0];
        }
        public static Ads GetUserAds(string login)
        {
            string sqlExpression = "EXEC GetUserAds @Login";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Login", login);

            string rezult = ReturnData(sqlExpression);
            Ads ads = new Ads
            {
                Ad = JsonConvert.DeserializeObject<List<Ad>>(rezult)
            };
            return ads;
        }
        public static void AddAd(Ad ad)
        {
            string sqlExpression = "EXECUTE AddAd @Ad, @Title, @DataCreation, @Picture, @Category, @Adress, @Type, @State, @IdUser";

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
                new SqlParameter("@IdUser", ad.Contact.IdUser)
            };

            SendRequest(sqlExpression, sqlParameters);
        }
        public static void ChangeAd(Ad ad)
        {
            string sqlExpression = "EXECUTE ChangeAd @Id, @Ad, @Title, @DataCreation, @Picture, @Category, @Adress, @Type, @State, @IdUser";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", ad.Id),
                new SqlParameter("@Ad", ad.Name),
                new SqlParameter("@Title", ad.Title),
                new SqlParameter("@DataCreation", DateTime.Now),
                new SqlParameter("@Picture", ad.Picture),
                new SqlParameter("@Category", ad.IdCategory()),
                new SqlParameter("@Type", ad.IdType()),
                new SqlParameter("@State", ad.IdState()),
                new SqlParameter("@Adress", ad.Adress),
                new SqlParameter("@IdUser", ad.Contact.IdUser),                
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeleteAd(int id, string login)
        {
            string sqlExpression = "EXECUTE DeleteAd @Id, @Login";

            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
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
        public static void AddUser(Contact contact)
        {
            string sqlExpression = "EXECUTE AddUser @Login, @Password, @Name, @Role, @Mail";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", contact.Login),
                new SqlParameter("@Password", contact.Password),
                new SqlParameter("@Name", contact.Name),
                new SqlParameter("@Role", contact.IdRole),
                new SqlParameter("@Mail", contact.Mails[0]),
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
        public static void ChangePhone( string phone, int idPhone)
        {
            string sqlExpression = "EXECUTE ChangePhone @IdPhone, @Phone";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdPhone",idPhone),
                new SqlParameter("@Phone",phone),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeletePhone( int idPhone)
        {
            string sqlExpression = "EXECUTE DeletePhone @IdPhone";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {                
                new SqlParameter("@IdPhone",idPhone),
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
        public static void ChangeMail(string login, string mail, int idMail)
        {
            string sqlExpression = "EXECUTE ChangeMail @Login, @IdMail, @Mail";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login",login),
                new SqlParameter("@IdMail",idMail),
                new SqlParameter("@Mail",mail),
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void DeleteMail( int idMail)
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
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
    }
}
