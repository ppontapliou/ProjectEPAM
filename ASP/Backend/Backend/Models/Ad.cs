using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Backend.Models
{

    public class Ad
    {

        public int Id { get; set; }
        public string NameAd { get; set; }
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

        }
        public List<SqlParameter> Params
        {
            get
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@Ad",NameAd),
                    new SqlParameter("@Title",Title),
                    new SqlParameter("@DateCreation",DateCreation),
                    new SqlParameter("@Picture",Picture),
                    new SqlParameter("@Category",Category),
                    new SqlParameter("@Adress",Adress),
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@State",State),
                    new SqlParameter("@Login",Contact.Login),
                    new SqlParameter("@Password",Contact.Password),
                };
                return SqlParameters;
            }
        }
        public List<SqlParameter> ParamsWithID
        {
            get
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@Ad",NameAd),
                    new SqlParameter("@Title",Title),
                    new SqlParameter("@DateCreation",DateCreation),
                    new SqlParameter("@Picture",Picture),
                    new SqlParameter("@Category",Category),
                    new SqlParameter("@Adress",Adress),
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@State",State),
                    new SqlParameter("@Login",Contact.Login),
                    new SqlParameter("@Password",Contact.Password),
                    new SqlParameter("@IdAd",Id)
                };
                return SqlParameters;
            }
        }
    }
}