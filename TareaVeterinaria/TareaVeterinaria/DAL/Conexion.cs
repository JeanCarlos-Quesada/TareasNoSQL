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

        public IList<Animalito> ListeAnimalitosPorTelefono(int telefono)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.ElemMatch(s => s.ElPropietario.LosContactos, x => x.NumeroTelefonico == telefono);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        public IList<Animalito> ListeAnimalitosPorTipoVacuna(String tipo)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.ElemMatch(s => s.Vacunas, x => x.tipo == tipo);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        public IList<Animalito> ListeAnimalitosPorEfectoSecundario(String efectoSecundario)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.ElemMatch(s => s.Vacunas, x => x.efectosSecundarios.Contains(efectoSecundario));
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }



        public IList<Animalito> ListarAnimalitosPorEdad(DateTime fechaInicial, DateTime fechaFinal)//22/07/2019
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Where(s => s.FechaNacimiento.Date >= fechaInicial && s.FechaNacimiento.Date <= fechaFinal);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }
    }
}
