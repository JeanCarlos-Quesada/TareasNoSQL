using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO.Objects
{
    public class Vacunas
    {
        [BsonElement("fecha")]
        public DateTime fecha { get; set; }

        [BsonElement("tipo")]
        public String tipo { get; set; }

        [BsonElement("efectosSecundarios")]
        public List<String> efectosSecundarios { get; set; }
    }
}
