using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class airbnb
    {
        [BsonElement("_id")]
        public string _id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("amenities")]
        public List<String> amenities { get; set; }

        [BsonElement("price")]
        public decimal price { get; set; }

        [BsonElement("reviews")]
        public IList<Reviews> reviews { get; set; }

        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }
    }
}
