using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nhom05DemoADB.Models
{
    public class SinhVienModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("HoTen")]
        public string HoTen { get; set; }

        [BsonElement("QueQuan")]
        public string QueQuan { get; set; }

        [BsonElement("SDT")]
        public string SDT { get; set; }


    }
}