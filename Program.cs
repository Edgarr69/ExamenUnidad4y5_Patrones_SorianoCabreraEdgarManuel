using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    class Program
    {
        static List<Barco> barcos = new List<Barco>();
        static int contadorId = 1;

        static void Main()
        {
            while (true)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\n--- SIMULADOR MARÍTIMO ---");
                Console.WriteLine("1. Crear Barco");
                Console.WriteLine("2. Listar Barcos");
                Console.WriteLine("3. Cambiar Control");
                Console.WriteLine("4. Cambiar Estado");
                Console.WriteLine("5. Acelerar Barco");
                Console.WriteLine("6. Detener Barco");
                Console.WriteLine("0. Salir");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": CrearBarco(); break;
                    case "2": Listar(); break;
                    case "3": CambiarControl(); break;
                    case "4": CambiarEstado(); break;
                    case "5": Acelerar(); break;
                    case "6": Detener(); break;
                    case "0": return;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            }
        }

        // ---------------------------------------------------------------------

        static void CrearBarco()
        {
            Console.WriteLine("1. Carguero");
            Console.WriteLine("2. Lancha Rápida");
            string tipo = Console.ReadLine();

            BarcoFactory factory = null;

            if (tipo == "1")
                factory = new CargueroFactory();
            else if (tipo == "2")
                factory = new LanchaFactory();
            else
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            Barco b = factory.CrearBarco();
            b.Id = contadorId++;
            b.Control = new ControlManual();
            b.Estado = new EnPuerto();

            barcos.Add(b);
            Console.WriteLine($"Barco creado: {b}");
        }

        // ---------------------------------------------------------------------

        static void Listar()
        {
            foreach (var b in barcos)
                Console.WriteLine(b);
        }

        // ---------------------------------------------------------------------

        static Barco Seleccionar()
        {
            Console.Write("ID del barco: ");
            int id;

            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID inválido.");
                return null;
            }

            Barco b = barcos.FirstOrDefault(x => x.Id == id);

            if (b == null)
                Console.WriteLine("No existe un barco con ese ID.");

            return b;
        }

        // ---------------------------------------------------------------------

        static void CambiarControl()
        {
            var b = Seleccionar();
            if (b == null) return;

            Console.WriteLine("1. Manual");
            Console.WriteLine("2. Automático");
            Console.WriteLine("3. IA");

            string op = Console.ReadLine();

            if (op == "1")
                b.Control = new ControlManual();
            else if (op == "2")
                b.Control = new ControlAutomatico();
            else if (op == "3")
                b.Control = new ControlIA();
            else
                Console.WriteLine("Opción inválida.");

            Console.WriteLine("Control cambiado.");
        }

        // ---------------------------------------------------------------------

        static void CambiarEstado()
        {
            var b = Seleccionar();
            if (b == null) return;

            Console.WriteLine("1. En Puerto");
            Console.WriteLine("2. En Navegación");
            Console.WriteLine("3. En Emergencia");

            string op = Console.ReadLine();

            if (op == "1")
                b.Estado = new EnPuerto();
            else if (op == "2")
                b.Estado = new EnNavegacion();
            else if (op == "3")
                b.Estado = new EnEmergencia();
            else
                Console.WriteLine("Opción inválida.");

            Console.WriteLine("Estado cambiado.");
        }

        // ---------------------------------------------------------------------

        static void Acelerar()
        {
            var b = Seleccionar();
            if (b != null)
                b.EjecutarAceleracion();
        }

        // ---------------------------------------------------------------------

        static void Detener()
        {
            var b = Seleccionar();
            if (b != null)
                b.EjecutarDetener();
        }
    }
}
