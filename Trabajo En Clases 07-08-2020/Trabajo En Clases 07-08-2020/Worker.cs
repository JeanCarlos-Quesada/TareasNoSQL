using DAL;
using DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_En_Clases_07_08_2020
{
    public class Worker
    {
        public void worker()
        {
            var laOpcion = string.Empty;
            ListeTodosLosAnimalitos();
            while (laOpcion != "X")
            {
                DesplegarMenu();
                laOpcion = LeaLaOpcion();
                switch (laOpcion)
                {
                    case "1":
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        String nameAnimalito = Console.ReadLine();
                        Animal(nameAnimalito);
                        Console.WriteLine("Digite el nuevo nombre del animal");
                        String nameNuevo = Console.ReadLine();
                        ActualizarNombre(nameNuevo, nameAnimalito);
                        Animal(nameNuevo);
                        break;
                    case "2":
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        String nombreAnimalito = Console.ReadLine();
                        Animal(nombreAnimalito);
                        Console.WriteLine("Digite el nuevo nombre del Propietario");
                        String nombrePro = Console.ReadLine();
                        Console.WriteLine("Digite el nuevo email del Propietario");
                        String emailPro = Console.ReadLine();
                        ActualizarPro(nombrePro, emailPro, nombreAnimalito);
                        Animal(nombreAnimalito);
                        break;
                    case "3":
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        nombreAnimalito = Console.ReadLine();
                        Contactos(nombreAnimalito);
                        Console.WriteLine("Digite el nuevo proveedor");
                        String proveedor = Console.ReadLine();
                        Console.WriteLine("Digite el nuevo telefono");
                        String telefono = Console.ReadLine();
                        ActualizarContacto(proveedor, telefono, nombreAnimalito);
                        Contactos(nombreAnimalito);
                        break;
                    case "4":
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        nombreAnimalito = Console.ReadLine();
                        Contactos(nombreAnimalito);
                        Console.WriteLine("Digite el telefono que desea eliminar");
                        telefono = Console.ReadLine();
                        DeleteContacto(telefono, nombreAnimalito);
                        Contactos(nombreAnimalito);
                        break;
                    case "5":
                        Console.WriteLine("Digite el nombre del animal que desea buscar");
                        nombreAnimalito = Console.ReadLine();
                        Vacunas(nombreAnimalito);
                        Console.WriteLine("Digite el tipo de vacuna");
                        String tipo = Console.ReadLine();
                        Console.WriteLine("Efecto Secundario");
                        String efectoSecundario = Console.ReadLine();
                        AgregarVacuna(tipo, efectoSecundario, nombreAnimalito);
                        Vacunas(nombreAnimalito);
                        break;
                    case "6":
                        ListeTodosLosAnimalitos();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ListeTodosLosAnimalitos()
        {
            var client = new Conexion();
            var laListaDeAnimalitos = client.ListarAnimalitos();
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        private void Animal(String nombre)
        {
            var client = new Conexion();
            var animalito = client.AnimalitosPorNombre(nombre);
            Console.WriteLine(string.Format("Id: {1}; Nombre: {0}; Nombre Propietario: {2}; Email Propietario: {3};",
                animalito.Nombre, animalito.AnimalitoId.ToString(), animalito.ElPropietario.Nombre, animalito.ElPropietario.DireccionElectronica));
        }

        private void Contactos(String nombre)
        {
            var client = new Conexion();
            var animalito = client.AnimalitosPorNombre(nombre);
            foreach (var cotactos in animalito.ElPropietario.LosContactos)
            {
                Console.WriteLine(string.Format("Proveedor: {0}; Telefono: {1}",
                    cotactos.Proveedor, cotactos.NumeroTelefonico));
            }
        }

        private void Vacunas(String nombre)
        {
            var client = new Conexion();
            var animalito = client.AnimalitosPorNombre(nombre);
            foreach (var vacuna in animalito.Vacunas) {
                Console.WriteLine(string.Format("Fecha: {0}; tipo: {1}; Efectos Secundario: {2}",
                    vacuna.fecha, vacuna.tipo, String.Join(" / ", vacuna.efectosSecundarios)));
            }
        }

        private void ActualizarNombre(String nombreNuevo, String nombreAnterior)
        {
            var client = new Conexion();
            client.UpdateNombreAnimal(nombreNuevo, nombreAnterior);
        }

        private void ActualizarPro(String nombrePro,String emmailPro, String nombreAnimal)
        {
            var client = new Conexion();
            client.UpdateNombrePropietario(nombrePro, emmailPro,nombreAnimal);
        }

        private void ActualizarContacto(String proveedor, String telefono, String nombreAnimal)
        {
            var client = new Conexion();
            client.UpdateContactos(proveedor, telefono, nombreAnimal);
        }

        private void DeleteContacto(String telefono, String nombreAnimal)
        {
            var client = new Conexion();
            client.DeleteContactos(telefono, nombreAnimal);
        }

        private void AgregarVacuna(String tipo, String efectoSecundario, String nombreAnimal)
        {
            var client = new Conexion();
            client.AgregarVacuna(tipo, efectoSecundario, nombreAnimal);
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

        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Actualizar Nombre Animal");
            Console.WriteLine("2.  Actualizar Nombre y Emmail Propietario");
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
