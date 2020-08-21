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

        public airbnb airbnbPorNombre(string elNombreAirbnb)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var expresssionFilter = Builders<airbnb>.Filter.Eq(x => x.name, elNombreAirbnb);
            var elResultado = collection.Find(expresssionFilter).FirstOrDefault();
            return elResultado;
        }

        public IList<airbnb> ConteinFacility(String facility)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var expresssionFilter = Builders<airbnb>.Filter.Where(s => s.amenities.Contains(facility));
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        public List<airbnb> airbnbRangoPrecio(decimal precioInicial, decimal precioFinal)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var expresssionFilter = Builders<airbnb>.Filter.Where(s => s.price >= precioInicial && s.price <= precioFinal);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        public void AgregarAmenities(String objeto, String elNombreAirbnb)
        {
            var airbnb = airbnbPorNombre(elNombreAirbnb);
            airbnb.amenities.Add(objeto);
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var builder = Builders<airbnb>.Filter;
            var filter = builder.Eq(s => s.name, elNombreAirbnb);
            var update = Builders<airbnb>.Update
             .Set(d => d.amenities, airbnb.amenities);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

        public void AgregarReviews(String name,DateTime date,String listing_id,String reviewer_id,String reviewer_name, String comments)
        {
            Random rnd = new Random();
            int idRandom = rnd.Next(1, 896489598);

            var airbnb = airbnbPorNombre(name);
            Reviews reviews = new Reviews();
            reviews._id = idRandom.ToString();
            reviews.date = date;
            reviews.listing_id = listing_id;
            reviews.reviewer_id = reviewer_id;
            reviews.reviewer_name = reviewer_name;
            reviews.comments = comments;

            airbnb.reviews.Add(reviews);
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<airbnb>("listingsAndReviews");
            var builder = Builders<airbnb>.Filter;
            var filter = builder.Eq(s => s.name, name);
            var update = Builders<airbnb>.Update
             .Set(d => d.reviews, airbnb.reviews);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

        public void UpdateComments(String newComentario, String elNombreAirbnb, string idReview)
        {
            var airbnb = airbnbPorNombre(elNombreAirbnb);
            airbnb.reviews.Where(s => s._id == idReview).FirstOrDefault().comments = newComentario;
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<airbnb>("listingsAndReviews");
            var builder = Builders<airbnb>.Filter;
            var filter = builder.Eq(s => s.name, elNombreAirbnb);
            var update = Builders<airbnb>.Update
             .Set(d => d.reviews, airbnb.reviews);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

    }
}
