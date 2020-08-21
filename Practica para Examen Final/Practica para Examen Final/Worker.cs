using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_para_Examen_Final
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
                        Console.WriteLine("Digite el nombre del airbnb");
                        String name = Console.ReadLine();
                        BuscarPorNombre(name);
                        break;
                    case "2":
                        Console.WriteLine("Digite el nombre del Amenities");
                        String facility = Console.ReadLine();
                        ConteinFacility(facility);
                        break;
                    case "3":
                        Console.WriteLine("Precio Inicial");
                        Decimal precioInicial = Decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Precio Final");
                        Decimal precioFinal = Decimal.Parse(Console.ReadLine());
                        airbnbRangoPrecio(precioInicial, precioFinal);
                        break;
                    case "4":
                        Console.WriteLine("Digite el nombre del airbnb");
                        String nombreairbnb = Console.ReadLine();
                        Console.WriteLine("digite la Amenities");
                        String Amenities = Console.ReadLine();
                        AgregarAmenities(Amenities, nombreairbnb);
                        break;
                    case "5":
                        Console.WriteLine("Digite el nombre del airbnb");
                        String nombre = Console.ReadLine();
                        Console.WriteLine("digite el listing_id");
                        String listing_id = Console.ReadLine();
                        Console.WriteLine("digite el reviewer_id");
                        String reviewer_id = Console.ReadLine();
                        Console.WriteLine("digite el reviewer_name");
                        String reviewer_name = Console.ReadLine();
                        Console.WriteLine("digite el comments");
                        String comments = Console.ReadLine();
                        AgregarReviews(nombre, listing_id, reviewer_id, reviewer_name, comments);
                        break;
                    case "6":
                        Console.WriteLine("Digite el nombre del airbnb");
                        String airbnbName = Console.ReadLine();
                        BuscarPorReviews(airbnbName);
                        Console.WriteLine("digite el id de la review");
                        String id = Console.ReadLine();
                        Console.WriteLine("digite el nuevo comentario");
                        String comentario = Console.ReadLine();
                        UpdateComments(comentario, airbnbName, id);
                        break;
                    default:
                        break;
                }
            }
        }
        private void BuscarPorNombre(String nombre)
        {
            var client = new Connection();
            var airbnb = client.airbnbPorNombre(nombre);
            if (airbnb != null)
            {
                Console.WriteLine(string.Format("Id: {0}; Nombre: {1};",
                    airbnb._id, airbnb.name));
            }
            else{
                Console.WriteLine("No se encontraron airbnb");
            }
        }

        private void ConteinFacility(String facility)
        {
            var client = new Connection();
            var airbnb = client.ConteinFacility(facility);

            if (airbnb.Count != 0) {
                foreach (var tmp in airbnb)
                {
                    Console.WriteLine(string.Format("Id: {0}; Nombre: {1}; Amenities{2}",
                  tmp._id, tmp.name, String.Join(" / ", tmp.amenities)));
                }
            }
            else
            {
                Console.WriteLine("No se encontraron airbnb");
            }
        }

        private void airbnbRangoPrecio(Decimal precioInicial, Decimal precioFinal)
        {
            var client = new Connection();
            var airbnb = client.airbnbRangoPrecio(precioInicial, precioFinal);

            if (airbnb.Count != 0)
            {
                foreach (var tmp in airbnb)
                {
                    Console.WriteLine(string.Format("Id: {0}; Nombre: {1}; precio{2}",
                  tmp._id, tmp.name, tmp.price));
                }
            }
            else
            {
                Console.WriteLine("No se encontraron airbnb");
            }
        }

        private void AgregarAmenities(String objeto, String elNombreAirbnb)
        {
            var client = new Connection();
            DateTime date = DateTime.Now;
            client.AgregarAmenities(objeto, elNombreAirbnb);

            Console.WriteLine("Se agrego la Amenities");
        }

        private void AgregarReviews(String name,String listing_id, String reviewer_id, String reviewer_name, String comments)
        {
            var client = new Connection();
            DateTime date = DateTime.Now;
            client.AgregarReviews(name, date, listing_id, reviewer_id, reviewer_name, comments);

            Console.WriteLine("Se agrego la review");
        }

        private void UpdateComments(String newComentario, String elNombreAirbnb, string idReview)
        {
            var client = new Connection();
            client.UpdateComments(newComentario, elNombreAirbnb, idReview);

            Console.WriteLine("Se actualizo la review");
        }

        private void BuscarPorReviews(String nombreAirbnb)
        {
            var client = new Connection();
            var airbnb = client.airbnbPorNombre(nombreAirbnb);
            if (airbnb != null)
            {
                foreach (var aux in airbnb.reviews)
                {
                    Console.WriteLine(string.Format("Id: {0}; Nombre: {1};",
                        aux._id, aux.reviewer_name));
                }
            }
            else
            {
                Console.WriteLine("No se encontraron airbnb");
            }
        }

        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1.  Buscar por nombre");
            Console.WriteLine("2.  Listado de todos los registros que contienen un facility en amenity");
            Console.WriteLine("3.  Listado de todos los registros por rango de precio.");
            Console.WriteLine("4.  Agregar Amenities");
            Console.WriteLine("5.  Agregar Review");
            Console.WriteLine("6.  Actualizar Comentario de una Review");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
