using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public class ControlManual : IControl
    {
        public string Nombre => "Manual";
        public void Controlar() => Console.WriteLine("El operador está controlando manualmente.");
    }
}
