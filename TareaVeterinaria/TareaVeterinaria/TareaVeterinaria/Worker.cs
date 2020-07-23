using DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareaVeterinaria
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
                        ListeAnimalitosPorEmailAproximado();
                        break;
                    case "2":
                        //ListeAnimalitosPorEdad();
                        break;
                    case "3":
                        ListeAnimalitosPorTelefono();
                        break;
                    case "4":
                        ListeAnimalitosPorTipoVacuna();
                        break;
                    case "5":
                        ListeAnimalitosPorEfectoSecundario();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ListeAnimalitosPorEmailAproximado()
        {
            Console.Write("Digite el email del propietario: ");
            var elNombreDelAnimalito = Console.ReadLine();
            var client = new DAL.Conexion();
            var laListaDeAnimalitos = client.ListarAnimalitosPorEmailAproximado(elNombreDelAnimalito);
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        private void ListeAnimalitosPorTelefono()
        {
            Console.Write("Digite el telefono del propietario: ");
            var telefono = Console.ReadLine();
            var client = new DAL.Conexion();
            var laListaDeAnimalitos = client.ListeAnimalitosPorTelefono(Int32.Parse(telefono));
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        private void ListeAnimalitosPorTipoVacuna()
        {
            Console.Write("Digite el tipo de vacuna: ");
            var tipo = Console.ReadLine();
            var client = new DAL.Conexion();
            var laListaDeAnimalitos = client.ListeAnimalitosPorTipoVacuna(tipo);
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }

        private void ListeAnimalitosPorEfectoSecundario()
        {
            Console.Write("Digite el efecto secundario: ");
            var efectoSecundario = Console.ReadLine();
            var client = new DAL.Conexion();
            var laListaDeAnimalitos = client.ListeAnimalitosPorEfectoSecundario(efectoSecundario);
            ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        }
        

        //private void ListeAnimalitosPorEdad()
        //{
        //    Console.Write("Digite la fecha Inicial (dd/mm/yyyy): ");
        //    var fechaInicio = Console.ReadLine();
        //    Console.Write("Digite la fecha Final (dd/mm/yyyy): ");
        //    var fechaFinal = Console.ReadLine();
        //    var client = new DAL.Conexion();
        //    var laListaDeAnimalitos = client.ListarAnimalitosPorEdad(fechaInicio, fechaFinal);
        //    ImprimirListadoDeAnimalitos(laListaDeAnimalitos);
        //}


        private void ImprimirListadoDeAnimalitos(IList<Animalito> laListaDeAnimalitos)
        {
            if (laListaDeAnimalitos.Count > 0)
            {
                Console.WriteLine("Lista de todos los animalitos:");
                foreach (var animalito in laListaDeAnimalitos)
                {
                    Console.WriteLine(string.Format("Id: {2}; Nombre: {0}; Tipo: {1}", animalito.Nombre, animalito.Tipo, animalito.AnimalitoId.ToString()));
                }
            }
            else
                Console.WriteLine("No se encontró ningún animalito.");
        }

        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Listado de animalitos por email aproximado del propietario.");
            Console.WriteLine("2. Listar por rango de edad.");
            Console.WriteLine("3. Listar por número exacto del propietario.");
            Console.WriteLine("4. Listar por tipo vacuna.");
            Console.WriteLine("5. Listar por efecto secundario.");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
