using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    public class Conection
    {
        public IMongoDatabase ConectarConBaseDeDatos()
        {
            var client = new MongoClient("mongodb+srv://root:root@myatlascluster-wregg.gcp.mongodb.net/sample_airbnb?retryWrites=true&w=majority");
            var database = client.GetDatabase("sample_airbnb");
            return database;
        }

        public airbnb airbnbPorNombre(string nombreAirbnb)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var expresssionFilter = Builders<airbnb>.Filter.Eq(x => x.name, nombreAirbnb);
            var elResultado = collection.Find(expresssionFilter).FirstOrDefault();
            return elResultado;
        }

        public airbnb airbnbRangoPrecio(decimal precioIncial, decimal precioFinal)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var expresssionFilter = Builders<airbnb>.Filter.Where(x => x.price >= precioIncial && x.price <= precioFinal);
            var elResultado = collection.Find(expresssionFilter).FirstOrDefault();
            return elResultado;
        }




    }
}
