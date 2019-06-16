using Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Models
{
    public class Repository : IRepository
    {
        public void DeleteAd(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAd(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAds()
        {
            throw new NotImplementedException();
        }

        public void PostAd([FromBody] string value)
        {
            throw new NotImplementedException();
        }
    }
}