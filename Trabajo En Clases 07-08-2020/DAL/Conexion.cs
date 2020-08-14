using DO.Objects;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IList<Animalito> ListarAnimalitos()
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var elResultado = collection.Find(_ => true).ToList();
            return elResultado;
        }

        public Animalito AnimalitosPorNombre(string elNombreDelAnimalito)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("Animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Eq(x => x.Nombre, elNombreDelAnimalito);
            var elResultado = collection.Find(expresssionFilter).FirstOrDefault();
            return elResultado;
        }

        public void UpdateNombreAnimal(String nombreAnimal, String nombreAnterior)
        {
            var animal = AnimalitosPorNombre(nombreAnterior);
            animal.Nombre = nombreAnimal;
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<Animalito>("Animalitos");
            var builder = Builders<Animalito>.Filter;
            var filter = builder.Eq(s => s.Nombre, nombreAnterior);
            var update = Builders<Animalito>.Update
             .Set(d => d.Nombre, animal.Nombre);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

        public void UpdateNombrePropietario(String nombre, String email, String nombreAnimal)
        {
            var animal = AnimalitosPorNombre(nombreAnimal);
            animal.ElPropietario.Nombre = nombre;
            animal.ElPropietario.DireccionElectronica = email;
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<Animalito>("Animalitos");
            var builder = Builders<Animalito>.Filter;
            var filter = builder.Eq(s => s.Nombre, nombreAnimal);
            var update = Builders<Animalito>.Update
             .Set(d => d.ElPropietario, animal.ElPropietario);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

        public void UpdateContactos(String proveedor, String numeroTelefonico, String nombreAnimal)
        {
            var animal = AnimalitosPorNombre(nombreAnimal);
            ContactoTelefonico contacto = new ContactoTelefonico();
            contacto.Proveedor = proveedor;
            contacto.NumeroTelefonico = Int32.Parse(numeroTelefonico);
            animal.ElPropietario.LosContactos.Add(contacto);
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<Animalito>("Animalitos");
            var builder = Builders<Animalito>.Filter;
            var filter = builder.Eq(s => s.Nombre, nombreAnimal);
            var update = Builders<Animalito>.Update
             .Set(d => d.ElPropietario.LosContactos, animal.ElPropietario.LosContactos);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

        public void DeleteContactos(String numeroTelefonico, String nombreAnimal)
        {
            int aux = Int32.Parse(numeroTelefonico);
            var animal = AnimalitosPorNombre(nombreAnimal);
            animal.ElPropietario.LosContactos.Remove(animal.ElPropietario.LosContactos.Where(s => s.NumeroTelefonico == aux).FirstOrDefault());
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<Animalito>("Animalitos");
            var builder = Builders<Animalito>.Filter;
            var filter = builder.Eq(s => s.Nombre, nombreAnimal);
            var update = Builders<Animalito>.Update
             .Set(d => d.ElPropietario.LosContactos, animal.ElPropietario.LosContactos);
            UpdateResult result = collection.UpdateOne(filter, update);
        }

        public void AgregarVacuna(String proveedor, String numeroTelefonico, String nombreAnimal)
        {
            var animal = AnimalitosPorNombre(nombreAnimal);
            ContactoTelefonico contacto = new ContactoTelefonico();
            contacto.Proveedor = proveedor;
            contacto.NumeroTelefonico = Int32.Parse(numeroTelefonico);
            animal.ElPropietario.LosContactos.Add(contacto);
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<Animalito>("Animalitos");
            var builder = Builders<Animalito>.Filter;
            var filter = builder.Eq(s => s.Nombre, nombreAnimal);
            var update = Builders<Animalito>.Update
             .Set(d => d.ElPropietario.LosContactos, animal.ElPropietario.LosContactos);
            UpdateResult result = collection.UpdateOne(filter, update);
        }
    }
}
