using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public abstract class BarcoFactory
    {
        public abstract Barco CrearBarco();
    }

    public class CargueroFactory : BarcoFactory
    {
        public override Barco CrearBarco() => new Carguero();
    }

    public class LanchaFactory : BarcoFactory
    {
        public override Barco CrearBarco() => new LanchaRapida();
    }
}
