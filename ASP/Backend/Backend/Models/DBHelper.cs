using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class DBHelper
    {
        static readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=EPAM_Project;Integrated Security=True";
        public delegate void AddMethod(string param);
        public static List<string[]> GetAds(string sqlExpression, int id = -1)
        {

            List<string[]> ads = new List<string[]>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                if (id >= 0)
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                }
                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] addedRow = new string[reader.FieldCount];
                        while (reader.FieldCount > i)
                        {
                            addedRow[i] = reader.GetValue(i).ToString();
                            i++;
                        }
                        i = 0;
                        ads.Add(addedRow);
                    }
                }
                reader.Close();
            }
            return ads;
        }
        public static Ad GetAd(int id)
        {
            return new Ads(DBHelper.GetAds("exec GetAd @Id", id)).Ad[0];
        }
        public static List<Ad> GetAds()
        {
            return new Ads(GetAds("exec GetAds")).Ad;
        }
        public static void DeleteAd(Ad ad)
        {
            string sqlExpression = "exec DeleteAd @Login, @Password, @IdAd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", ad.Contact.Login),
                new SqlParameter("@Password", ad.Contact.Password),
                new SqlParameter("@IdAd", ad.Id)
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        public static void AddAd(Ad ad)
        {
            string sqlExpression = "exec AddAd @Ad, @Title, @DateCreation, @Picture, @Category, @Adress, @Type, @State, @Login, @Password";
            SendRequest(sqlExpression, ad.Params);
        }
        public static void UpdateAd(Ad ad)
        {
            string sqlExpression = "exec UpdateAd @Ad, @Title, @DateCreation, @Picture, @Category, @Adress, @Type, @State, @Login, @Password, @IdAd";
            SendRequest(sqlExpression, ad.ParamsWithID);
        }
        public static void CerateContact(Contact contact)
        {
            string sqlExpression = "exec Authentication @Login, @Password";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login",contact.Login),
                new SqlParameter("@Login",contact.Password)
            };
            SendRequest(sqlExpression, sqlParameters);
        }
        private static void SendRequest(string sqlExpression, List<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                //command.Parameters.Add(new SqlParameter("@Login", contact.Login));
                foreach (var param in sqlParameters)
                {
                    command.Parameters.Add(param);
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public static void GetContactsInfo(string sqlExpression, AddMethod contact, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", id));

                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] addedRow = new string[reader.FieldCount];
                        while (reader.FieldCount > i)
                        {
                            addedRow[i] = reader.GetValue(i).ToString();
                            i++;
                        }
                        i = 0;
                        contact(addedRow[0]);
                    }
                }
                reader.Close();
            }
        }
        public static string GetContactsInfo(string sqlExpression, Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add(new SqlParameter("@Login", contact.Login));
                command.Parameters.Add(new SqlParameter("@Password", contact.Password));

                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] addedRow = new string[reader.FieldCount];
                        while (reader.FieldCount > i)
                        {
                            addedRow[i] = reader.GetValue(i).ToString();
                            i++;
                        }
                        i = 0;
                        return (addedRow[0]);
                    }
                }
                reader.Close();
                return null;
            }
        }
        public static void ChangeUserName(Contact contact)
        {
            string sqlExpression = "exec ChangeUserName @Login, @Password, @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Login", contact.Login));
                command.Parameters.Add(new SqlParameter("@Password", contact.Password));
                command.Parameters.Add(new SqlParameter("@Name", contact.Name));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void DeleteUser(int id, Contact contact)
        {
            string sqlExpression = "exec DeleteUser @Login, @Password, @UserID ";
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", contact.Login),
                new SqlParameter("@Password", contact.Password),
                new SqlParameter("@UserId", id)
            };
            SendRequest(sqlExpression, sqlParameters);
        }
    }
}