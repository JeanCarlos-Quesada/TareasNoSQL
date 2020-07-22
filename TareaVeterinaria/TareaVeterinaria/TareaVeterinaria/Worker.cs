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
                        ListeLasColecciones();
                        break;
                    case "2":
                        ListeTodosLosAnimalitos();
                        break;
                    case "3":
                        ListeAnimalitosPorNombre();
                        break;
                    default:
                        break;
                }
            }
        }

        private void DesplegarMenu()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Listar las colecciones.");
            Console.WriteLine("2. Listar todos los animalitos.");
            Console.WriteLine("3. Listar los animalitos por nombre.");
            Console.WriteLine("X.  Salir");
        }

        private string LeaLaOpcion()
        {
            string elResultado = Console.ReadLine();
            return elResultado;
        }
    }
}
