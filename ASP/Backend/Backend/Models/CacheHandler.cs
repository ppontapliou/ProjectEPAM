using Backend.Interfaces;
using Backend.Models.DBModelsHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;

namespace Backend.Models
{
    public class CacheHandler : ICacheHandler
    {
        public CacheHandler()
        {

        }
        public void InsertInCache(string name, object value)
        {
            Cache cache = new Cache();
            cache.Insert(name, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
        }
        public List<Parameter> GetCategories()
        {
            Cache cache = new Cache();
            List<Parameter> categories = cache["Categories"] as List<Parameter>;
            if (categories == null)
            {
                categories = RefreshCategories();
            }
            return categories;
        }
        public List<Parameter> RefreshCategories()
        {
            DBModelHelper helper = new DBModelHelper();
            var categories = helper.Categories.Select(c => new Parameter()
            {
                Id = c.Id,
                Name = c.Category
            }).ToList();
            InsertInCache("Categories", categories);
            return categories;
        }
        public List<Parameter> GetTypes()
        {
            Cache cache = new Cache();
            List<Parameter> types = cache["Types"] as List<Parameter>;
            if (types == null)
            {
                types = RefreshTypes();
            }
            return types;
        }
        public List<Parameter> RefreshTypes()
        {
            DBModelHelper helper = new DBModelHelper();
            var types = helper.Types.Select(c => new Parameter()
            {
                Id = c.Id,
                Name = c.Type
            }).ToList();
            InsertInCache("Types", types);
            return types;
        }
        public List<Parameter> GetStates()
        {
            Cache cache = new Cache();
            List<Parameter> states = cache["States"] as List<Parameter>;
            if (states == null)
            {
                states = RefreshStates();
            }
            return states;
        }
        public List<Parameter> RefreshStates()
        {
            DBModelHelper helper = new DBModelHelper();
            var states = helper.States.Select(c => new Parameter()
            {
                Id = c.Id,
                Name = c.State
            }).ToList();
            InsertInCache("States", states);
            return states;
        }
    }
}