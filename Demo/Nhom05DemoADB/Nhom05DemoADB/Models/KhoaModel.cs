using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nhom05DemoADB.Models
{
    public class KhoaModel
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("TenKhoa")]
        public string TenKhoa { get; set; }

        [BsonElement("NamThanhLap")]
        public string NamThanhLap { get; set; }

        [BsonElement("DiaChi")]
        public string DiaChi { get; set; }

    }
}