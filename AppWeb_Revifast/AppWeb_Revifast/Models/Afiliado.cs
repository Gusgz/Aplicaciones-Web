using System;
using System.Collections.Generic;

namespace AppWeb_Revifast.Models
{
    public partial class Afiliado
    {
        public Afiliado()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int IdAfiliado { get; set; }
        public string Correo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
