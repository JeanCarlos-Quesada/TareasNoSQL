﻿using DO.Objects;
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
                        //ListeTodosLosAnimalitos();
                        break;
                    case "3":
                        //ListeAnimalitosPorNombre();
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
