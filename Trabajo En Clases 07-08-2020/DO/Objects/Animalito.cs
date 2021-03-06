﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO.Objects
{
    [BsonIgnoreExtraElements]
    public class Animalito
    {
        [BsonId]
        public ObjectId AnimalitoId { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }

        [BsonElement("fechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [BsonExtraElements]
        public BsonDocument Metadata { get; set; }

        [BsonElement("propietario")]
        public Propietario ElPropietario { get; set; }

        [BsonElement("fotos")]
        public BsonArray Fotos { get; set; }

        [BsonElement("vacunas")]
        public IList<Vacunas> Vacunas { get; set; }
    }
}
