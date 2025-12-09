using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public interface IEstado
    {
        string NombreEstado { get; }
        void Acelerar(Barco barco);
        void Detener(Barco barco);
    }
}
