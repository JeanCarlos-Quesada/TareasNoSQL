using DO.Objects;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
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
            var client = new MongoClient("mongodb+srv://rwuser:12345@myatlascluster-as0q0.gcp.mongodb.net/veterinaria?retryWrites=true&w=majority");
            var database = client.GetDatabase("veterinaria");
            return database;
        }

        public IList<Animalito> ListarAnimalitosPorNombre(string elNombreDelAnimalito)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Regex(x => x.Nombre, elNombreDelAnimalito);
            var elResultado = collection.Find(expresssionFilter).ToList();
            return elResultado;
        }

        public IList<Fotos> ListaFotos(string nombreAnimal)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            var collection = laBaseDeDatos.GetCollection<Animalito>("animalitos");
            var expresssionFilter = Builders<Animalito>.Filter.Eq(s => s.Nombre, nombreAnimal);
            var elResultado = collection.Find(expresssionFilter).ToList().FirstOrDefault();

            IList<Fotos> fotos = new List<Fotos>();

            foreach (var i in elResultado.Fotos)
            {
                var collection02 = laBaseDeDatos.GetCollection<Fotos>("fs.files");
                var expresssionFilter02 = Builders<Fotos>.Filter.Eq(s => s.FotoId, i);
                var elResultado02 = collection02.Find(expresssionFilter02).ToList().FirstOrDefault();
                fotos.Add(elResultado02);
            }
            return fotos;
        }

        public void DownloadFileAsync(String fileName, String ruta)
        {
            var laBaseDeDatos = ConectarConBaseDeDatos();
            IGridFSBucket bucket = new GridFSBucket(laBaseDeDatos);
            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Filename, fileName);

            var searchResult = bucket.Find(filter);
            var fileEntry = searchResult.FirstOrDefault();
            var file = ruta + fileName;

            using (Stream fs = new FileStream(file, FileMode.CreateNew,FileAccess.Write))
            {
                bucket.DownloadToStream(fileEntry.Id, fs);
                fs.Close();
            }
        }

        public ObjectId SubirFileAsync(String archivo,String fileName ,MetadataDeFotos laMetadata)
        {
            var database = ConectarConBaseDeDatos();
            IGridFSBucket bucket = new GridFSBucket(database);
            Stream strem = File.Open(archivo, FileMode.Open);
            var options = new GridFSUploadOptions()
            {
                Metadata = new BsonDocument
                {
                    {"descripcion",laMetadata.Descripcion},
                    {"fechaYHora",laMetadata.FechaYHora }
                }
            };

            var id = bucket.UploadFromStream(fileName, strem, options);
            return id;
        }

        public void UpdateAnimal(String nombreAnimal, ObjectId idFoto)
        {
            var animal = ListarAnimalitosPorNombre(nombreAnimal).Where(s => s.Nombre == nombreAnimal).FirstOrDefault();
            animal.Fotos.Add(idFoto);
            var database = ConectarConBaseDeDatos();
            var collection = database.GetCollection<Animalito>("animalitos");
            var builder = Builders<Animalito>.Filter;
            var filter = builder.Eq(s => s.Nombre, nombreAnimal);
            var update = Builders<Animalito>.Update
             .Set(d => d.Fotos, animal.Fotos);
            UpdateResult result = collection.UpdateOne(filter, update);
        }
    }
}
