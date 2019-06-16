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
        public static List<string[]> GetAds(string sqlExpression)
        {
            List<string[]> ads = new List<string[]>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
        public static void DeleteAd(int id)
        {
            string sqlExpression = "exec DeleteAd " + id;
            SendRequest(sqlExpression);
        }
        public static void AddAd(Ad ad)
        {
            string sqlExpression = "exec AddAd " + ad.ToString();
            SendRequest(sqlExpression);
        }
        public static void UpdateAd(Ad ad)
        {
            string sqlExpression = "exec UpdateAd " + ad.ToStringWithId();
            SendRequest(sqlExpression);
        }
        public static void CerateContact(Contact contact)
        {
            string sqlExpression = "exec Authentication " + contact.LoginAndPassword;
            SendRequest(sqlExpression);
        }
        private static void SendRequest(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                
            }
        }
        public delegate void AddMethod(string param);

        public static void GetContactsInfo(string sqlExpression, AddMethod contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
        public static string GetContactsInfo(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
                command.Parameters.Add(new SqlParameter("@Login",contact.Login));
                command.Parameters.Add(new SqlParameter("@Password", contact.Password));
                command.Parameters.Add(new SqlParameter("@Name", contact.Name));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}