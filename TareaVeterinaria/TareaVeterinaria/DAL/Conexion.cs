using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Conexion
    {
        public IMongoDatabase ConectarConBaseDeDatos()
        {
            var client = new MongoClient("mongodb+srv://rwuser:12345@myatlascluster-as0q0.gcp.mongodb.net/veterinaria?retryWrites=true&w=majority");
            var database = client.GetDatabase("veterinaria");
            return database;
        }
    }
}
