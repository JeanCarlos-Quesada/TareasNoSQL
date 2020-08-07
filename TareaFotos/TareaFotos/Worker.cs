using DAL;
using DO.Objects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaFotos
{
    public class Worker
    {
        public void worker()
        {
            var laOpcion = string.Empty;
            while (laOpcion != "X")
            {
                DesplegarMenu();
                laOpcion = LeaLaOpcion();
                switch (laOpcion)
                {
                    case "1":
                        ListeTodosLosAnimalitos();
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        String nameAnimalito = Console.ReadLine();
                        ListadoFotos(nameAnimalito);
                        DownloadFileAsync();
                        break;
                    case "2":
                        ListeTodosLosAnimalitos();
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        String nombreAnimalito = Console.ReadLine();
                        ObjectId aux = SubirFileAsync();
                        ActualizarListFotos(nombreAnimalito, aux);
                        ListadoFotos(nombreAnimalito);
                        break;
                        default:
                        break;
                }
            }
        }

        private void ListeTodosLosAnimalitos()
        {
            Console.WriteLine("Digite el aproximado nombre del animal");
            string nameAnimalito = Console.ReadLine();
            var client = new Conexion();
            var laListaDeAnimalitos = client.ListarAnimalitosPorNombre(nameAnimalito);
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        private void ListadoFotos(String nameAnimalito)
        {
            var client = new Conexion();
            var laListaDeAnimalitos = client.ListaFotos(nameAnimalito);
            ImprimirListadoDeFotos(laListaDeAnimalitos);
        }

        private void DownloadFileAsync()
        {

            Console.WriteLine("Digite el nombre del archivo");
            String fileName = Console.ReadLine();
            Console.WriteLine("Digite la ruta para guardar el archivo");
            String ruta = Console.ReadLine();
            var client = new Conexion();
            client.DownloadFileAsync(fileName, ruta);
        }

        private ObjectId SubirFileAsync()
        {
            Console.WriteLine("Digite el nombre del archivo");
            String fileName = Console.ReadLine();
            Console.WriteLine("Digite la ruta del el archivo");
            String ruta = Console.ReadLine();
            Console.WriteLine("Digite la descripcion del archivo");
            String descripcion = Console.ReadLine();
            Console.WriteLine("Digite la fecha del archivo");
            String fechaHora = Console.ReadLine();
            MetadataDeFotos metadata = new MetadataDeFotos();
            metadata.Descripcion = descripcion;
            metadata.FechaYHora = fechaHora;
            var client = new Conexion();
            return client.SubirFileAsync(ruta, fileName, metadata);
        }

        private void ActualizarListFotos(String nombreAnimal, ObjectId aux)
        {
            var client = new Conexion();
            client.UpdateAnimal(nombreAnimal, aux);
        }

        private void ImprimirListadoDeAnimalitos(IList<Animalito> laListaDeAnimalitos)
        {
            if (laListaDeAnimalitos.Count > 0)
            {
                Console.WriteLine("Lista de todos los animalitos:");
                foreach (var animalito in laListaDeAnimalitos)
                {
                    Console.WriteLine(string.Format("Id: {1}; Nombre: {0};", animalito.Nombre, animalito.AnimalitoId.ToString()));
                }
            }
            else
                Console.WriteLine("No se encontró ningún animalito.");
        }

        private void ImprimirListadoDeFotos(IList<Fotos> laListaDeAnimalitos)
        {
            if (laListaDeAnimalitos.Count > 0)
            {
                Console.WriteLine("Lista de todos las las fotos:");
                foreach (var animalito in laListaDeAnimalitos)
                {
                    Console.WriteLine(string.Format("Id: {1}; Nombre: {0};", animalito.filename, animalito.FotoId.ToString()));
                }
            }
            else
                Console.WriteLine("No se encontró ningún animalito.");
        }

        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Listar todos los animalitos para descargar imagenes.");
            Console.WriteLine("2. Listar todos los animalitos para subir imagenes.");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
