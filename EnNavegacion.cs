using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public class EnNavegacion : IEstado
    {
        public string NombreEstado => "En Navegacion";

        public void Acelerar(Barco barco)
        {
            Console.WriteLine($"{barco.Nombre} acelerando a velocidad de crucero");
        }

        public void Detener(Barco barco)
        {
            Console.WriteLine($"{barco.Nombre} reduciendo velocidad y deteniendose");
        }
    }
}
