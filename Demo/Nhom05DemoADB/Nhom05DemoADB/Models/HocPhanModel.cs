using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nhom05DemoADB.Models
{
    public class HocPhanModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("TenHP")]
        public string TenHP { get; set; }

        [BsonElement("TinChi")]
        public string TinChi { get; set; }

        [BsonElement("MoTa")]
        public string MoTa { get; set; }

    }
}