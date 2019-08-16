using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    interface ICacheHandler
    {
        void InsertInCache(string name, object value);
        List<Parameter> GetCategories();
        List<Parameter> RefreshCategories();
        List<Parameter> GetTypes();
        List<Parameter> RefreshTypes();
        List<Parameter> GetStates();
        List<Parameter> RefreshStates();
    }
}
