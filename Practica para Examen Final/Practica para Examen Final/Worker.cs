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

                        break;
                    case "2":
                        ConteinFacility();
                        break;
                    case "3":
                        
                        break;
                    case "4":
                      
                        break;
                    case "5":
                       
                        break;
                    case "6":
                        
                        break;
                    default:
                        break;
                }
            }
        }
        //private void BuscarPorNombre(String nombre)
        //{
        //    var client = new Connection();
        //    var airbnb = client.ConteinFacility(nombre);
        //    Console.WriteLine(string.Format("Id: {1}; Nombre: {0};",
        //        airbnb.Nombre, animalito.AnimalitoId.ToString(), animalito.ElPropietario.Nombre, animalito.ElPropietario.DireccionElectronica));
        //}

        private void ConteinFacility()
        {
            var client = new Connection();
            var airbnb = client.aux();
            foreach (var tmp in airbnb)
            {
                Console.WriteLine(string.Format("Id: {0}; Nombre: {1}; Amenities{2}",
              tmp._id, tmp.name, String.Join(" / ",tmp.amenities)));
            }
        }


        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1.  Buscar por nombre");
            Console.WriteLine("2.  Listado de todos los registros que contienen un facility en amenity");
            Console.WriteLine("3.  Agregar Contacto");
            Console.WriteLine("4.  Eliminar Contacto");
            Console.WriteLine("5.  Agregar Vacuna");
            Console.WriteLine("6.  Lista Animales");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
