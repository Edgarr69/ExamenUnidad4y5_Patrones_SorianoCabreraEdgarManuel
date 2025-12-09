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

        static List<Evento> historialEventos = new List<Evento>(); 

        static void Main()
        {
 
            EventBus.OnEvento += (mensaje, tipo) =>
            {
                historialEventos.Add(new Evento { Mensaje = mensaje, Tipo = tipo });
            };

            while (true)
            {
                Console.Clear();
                MostrarEventosRecientes();

                Console.WriteLine("--- SIMULADOR MARÍTIMO ---");
                Console.WriteLine("1. Crear Barco");
                Console.WriteLine("2. Listar Barcos");
                Console.WriteLine("3. Cambiar Control");
                Console.WriteLine("4. Cambiar Estado");
                Console.WriteLine("5. Acelerar Barco");
                Console.WriteLine("6. Detener Barco");
                Console.WriteLine("0. Salir");

                Console.Write("\nSeleccione una opción: ");
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
                    default: EventBus.Publicar("Opción inválida", TipoEvento.Alerta); Pausa(); break;
                }
            }
        }


        static void MostrarEventosRecientes(int max = 5)
        {
            Console.WriteLine("--- Eventos recientes ---");
            var recientes = historialEventos.Skip(Math.Max(0, historialEventos.Count - max));
            foreach (var e in recientes)
            {
                switch (e.Tipo)
                {
                    case TipoEvento.Normal: Console.ForegroundColor = ConsoleColor.Green; break;
                    case TipoEvento.Cambio: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case TipoEvento.Alerta: Console.ForegroundColor = ConsoleColor.Red; break;
                }
                Console.WriteLine(e.Mensaje);
                Console.ResetColor();
            }
            Console.WriteLine("------------------------\n");
        }

        static void CrearBarco()
        {
            Console.Clear();
            Console.WriteLine("--- CREAR BARCO ---");
            Console.WriteLine("1. Carguero");
            Console.WriteLine("2. Lancha Rápida");
            Console.Write("Seleccione tipo: ");
            string tipo = Console.ReadLine();

            BarcoFactory factory = null;

            if (tipo == "1") factory = new CargueroFactory();
            else if (tipo == "2") factory = new LanchaFactory();
            else { EventBus.Publicar("Tipo inválido", TipoEvento.Alerta); Pausa(); return; }

            Barco b = factory.CrearBarco();
            b.Id = contadorId++;
            b.Control = new ControlManual();
            b.Estado = new EnPuerto();

            barcos.Add(b);
            EventBus.Publicar($"Barco creado: {b}", TipoEvento.Normal);
            Pausa();
        }

        static void Listar()
        {
            Console.Clear();
            MostrarEventosRecientes();
            Console.WriteLine("--- LISTA DE BARCOS ---");

            if (barcos.Count == 0)
                Console.WriteLine("No hay barcos registrados.");
            else
                foreach (var b in barcos)
                    Console.WriteLine(b);

            Pausa();
        }

        static Barco Seleccionar()
        {
            Console.Write("\nID del barco: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                EventBus.Publicar("ID inválido", TipoEvento.Alerta);
                return null;
            }

            var b = barcos.FirstOrDefault(x => x.Id == id);
            if (b == null) EventBus.Publicar("No existe un barco con ese ID", TipoEvento.Alerta);
            return b;
        }

        static void CambiarControl()
        {
            Console.Clear();
            MostrarEventosRecientes();
            Console.WriteLine("--- CAMBIAR CONTROL ---");

            var b = Seleccionar();
            if (b == null) { Pausa(); return; }

            Console.WriteLine("\n1. Manual");
            Console.WriteLine("2. Automático");
            Console.WriteLine("3. IA");
            Console.Write("Seleccione: ");
            string op = Console.ReadLine();

            if (op == "1") { b.Control = new ControlManual(); EventBus.Publicar($"Control de {b.Nombre} cambiado a Manual", TipoEvento.Cambio); }
            else if (op == "2") { b.Control = new ControlAutomatico(); EventBus.Publicar($"Control de {b.Nombre} cambiado a Automático", TipoEvento.Cambio); }
            else if (op == "3") { b.Control = new ControlIA(); EventBus.Publicar($"Control de {b.Nombre} cambiado a IA", TipoEvento.Cambio); }
            else EventBus.Publicar("Opción inválida", TipoEvento.Alerta);

            Pausa();
        }

        static void CambiarEstado()
        {
            Console.Clear();
            MostrarEventosRecientes();
            Console.WriteLine("--- CAMBIAR ESTADO ---");

            var b = Seleccionar();
            if (b == null) { Pausa(); return; }

            Console.WriteLine("\n1. En Puerto");
            Console.WriteLine("2. En Navegación");
            Console.WriteLine("3. En Emergencia");
            Console.Write("Seleccione: ");
            string op = Console.ReadLine();

            if (op == "1") { b.Estado = new EnPuerto(); EventBus.Publicar($"Estado de {b.Nombre} cambiado a En Puerto", TipoEvento.Cambio); }
            else if (op == "2") { b.Estado = new EnNavegacion(); EventBus.Publicar($"Estado de {b.Nombre} cambiado a En Navegación", TipoEvento.Cambio); }
            else if (op == "3") { b.Estado = new EnEmergencia(); EventBus.Publicar($"Estado de {b.Nombre} cambiado a En Emergencia", TipoEvento.Alerta); }
            else EventBus.Publicar("Opción inválida", TipoEvento.Alerta);

            Pausa();
        }

        static void Acelerar()
        {
            Console.Clear();
            MostrarEventosRecientes();
            Console.WriteLine("--- ACELERAR ---");

            var b = Seleccionar();
            if (b != null)
            {
                b.EjecutarAceleracion();
                EventBus.Publicar($"{b.Nombre} (ID:{b.Id}) aceleró en estado {b.Estado.NombreEstado}", TipoEvento.Normal);
            }

            Pausa();
        }

        static void Detener()
        {
            Console.Clear();
            MostrarEventosRecientes();
            Console.WriteLine("--- DETENER ---");

            var b = Seleccionar();
            if (b != null)
            {
                b.EjecutarDetener();
                EventBus.Publicar($"{b.Nombre} (ID:{b.Id}) se detuvo en estado {b.Estado.NombreEstado}", TipoEvento.Normal);
            }

            Pausa();
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresiona ENTER para continuar...");
            Console.ReadLine();
        }
    }


    public static class EventBus
    {
        public static event Action<string, TipoEvento> OnEvento;

        public static void Publicar(string mensaje, TipoEvento tipo)
        {
            if (OnEvento != null)
                OnEvento.Invoke(mensaje, tipo);
        }
    }

    public enum TipoEvento
    {
        Normal,
        Cambio,
        Alerta
    }

    public class Evento
    {
        public string Mensaje { get; set; }
        public TipoEvento Tipo { get; set; }
    }
}

