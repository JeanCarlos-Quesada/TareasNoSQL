using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Reviews
    {
        [BsonElement("_id")]
        public string _id { get; set; }

        [BsonElement("date")]
        public DateTime date { get; set; }

        [BsonElement("listing_id")]
        public String listing_id { get; set; }

        [BsonElement("reviewer_id")]
        public String reviewer_id { get; set; }

        [BsonElement("reviewer_name")]
        public String reviewer_name { get; set; }
        [BsonElement("comments")]
        public String comments { get; set; }

    }
}
