using DO.Objects;
using MongoDB.Driver;
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
            var client = new MongoClient("mongodb+srv://root:root@myatlascluster-wregg.gcp.mongodb.net/Veterinaria?retryWrites=true&w=majority");
            var database = client.GetDatabase("Veterinaria");
            return database;
        }

        public IList<Animalito> ListarAnimalitosPorEmailAproximado(string email)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Regex(x => x.ElPropietario.DireccionElectronica, email);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        .FechaNacimiento.Year - DateTime.Now.Year) ) && ((s.FechaNacimiento.Year - DateTime.Now.Year) <= )
        public IList<Animalito> ListarAnimalitosPorEdad(int edadInicial, int edadFinal)//22/07/2019
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Where(s => ((DateTime.Compare(s.FechaNacimiento,DateTime.Now)) >= edadInicial) && ((DateTime.Compare(s.FechaNacimiento, DateTime.Now)) <= edadFinal));
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }
    }
}
