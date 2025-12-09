using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public class EnEmergencia : IEstado
    {
        public string NombreEstado => "En Emergencia";

        public void Acelerar(Barco barco)
        {
            Console.WriteLine($"⚠ Emergencia: {barco.Nombre} solo puede maniobrar, aceleración limitada.");
        }

        public void Detener(Barco barco)
        {
            Console.WriteLine($"⚠ {barco.Nombre} no puede detenerse completamente por emergencia.");
        }
    }
}
