using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CarreraDTO
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public List<JugadorDTO> Participantes { get; set; }
        public CaballoDTO CaballoGanador { get; set; }
        public List<JugadorDTO> Ganadores { get; set; }

        public CarreraDTO()
        {
            Participantes = new List<JugadorDTO>();
            Ganadores = new List<JugadorDTO>();
        }
    }
}