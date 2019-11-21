using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MongoDB.Driver;

namespace Nhom05DemoADB.App_Start
{
    public class MongoDBConnect
    {      
        public IMongoDatabase database;

        public MongoDBConnect()
        {
            var mongoClinet = new MongoClient(ConfigurationManager.AppSettings["MongoDBHost"]);
            database = mongoClinet.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);
        }

    }
}