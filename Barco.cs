using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public abstract class Barco
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Bridge
        public IControl Control { get; set; }

        // State
        public IEstado Estado { get; set; }

        public void EjecutarAceleracion() => Estado.Acelerar(this);
        public void EjecutarDetener() => Estado.Detener(this);

        public override string ToString()
        {
            return $"[{Id}] {Nombre} | Control: {Control.Nombre} | Estado: {Estado.NombreEstado}";
        }
    }
}
