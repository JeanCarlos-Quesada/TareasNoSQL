using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO.Objects
{
    [BsonIgnoreExtraElements]
    public class Fotos
    {
        [BsonId]
        public ObjectId FotoId { get; set; }
        [BsonElement("filename")]
        public string filename { get; set; }
        [BsonElement("metadata")]
        public MetadataDeFotos LaMetadata { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class MetadataDeFotos
    {
        public string Descripcion { get; set; }
        public string FechaYHora { get; set; }
    }
}

