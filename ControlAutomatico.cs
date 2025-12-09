using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public class ControlAutomatico : IControl
    {
        public string Nombre => "Automatico";
        public void Controlar() => Console.WriteLine("Sistema automatico navegando");
    }
}
