using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_unidad_4_y_5
{
    public class EnPuerto : IEstado
    {
        public string NombreEstado => "En Puerto";

        public void Acelerar(Barco barco)
        {
            Console.WriteLine($"El barco {barco.Nombre} no puede acelerar, esta amarrado en puerto");
        }

        public void Detener(Barco barco)
        {
            Console.WriteLine("El barco ya esta detenido en puerto");
        }
    }
}
