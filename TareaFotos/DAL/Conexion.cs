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
    }
}
