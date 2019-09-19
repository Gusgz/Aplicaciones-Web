using System;
using System.Collections.Generic;

namespace AppWeb_Revifast.Models
{
    public partial class Conductor
    {
        public Conductor()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int IdConductor { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public long? Celular { get; set; }
        public string Correo { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
