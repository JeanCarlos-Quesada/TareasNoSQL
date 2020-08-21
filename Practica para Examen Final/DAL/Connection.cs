using DO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Connection
    {
        public IMongoDatabase ConectarConBaseDeDatos()
        {
            var client = new MongoClient("mongodb+srv://root:root@myatlascluster-wregg.gcp.mongodb.net/sample_airbnb?retryWrites=true&w=majority");
            var database = client.GetDatabase("sample_airbnb");
            return database;
        }

        public IList<airbnb> ConteinFacility()
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var expresssionFilter = Builders<airbnb>.Filter.Where(s => s.amenities.Contains("facility"));
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        public IList<airbnb> aux()
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var elResultado = collection.Find(_ => true).ToList();
            return elResultado;
        }
    }
}
