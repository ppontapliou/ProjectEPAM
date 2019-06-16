using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Interfaces
{
    public interface IRepository
    {
        string GetAds();
        string GetAd(int id);
        void DeleteAd(int id);
        void PostAd([FromBody]string value);
    }
}
