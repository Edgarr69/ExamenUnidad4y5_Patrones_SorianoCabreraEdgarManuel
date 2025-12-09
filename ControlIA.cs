using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public class ControlIA : IControl
    {
        public string Nombre => "IA";
        public void Controlar() => Console.WriteLine("IA tomando decisiones de navegación.");

    }
}
