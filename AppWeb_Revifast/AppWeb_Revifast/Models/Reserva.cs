using System;
using System.Collections.Generic;

namespace AppWeb_Revifast.Models
{
    public partial class Reserva
    {
        public int IdReserva { get; set; }
        public int? IdConductor { get; set; }
        public int? IdAfiliado { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Afiliado IdAfiliadoNavigation { get; set; }
        public virtual Conductor IdConductorNavigation { get; set; }
    }
}
