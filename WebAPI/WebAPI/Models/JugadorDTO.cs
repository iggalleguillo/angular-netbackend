using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class JugadorDTO
    {
        public int IdJugador { get; set; }
        public string Nombre { get; set; }
        public decimal Saldo { get; set; }
        public int CaballoApostado { get; set; }
        public decimal CantidadApostada { get; set; }
    }
}