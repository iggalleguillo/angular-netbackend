using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class JugadorRepository
    {
        public async Task<Jugadores> CrearJugador(JugadorDTO jugadorDto)
        {
            using (CarrerasBDEntities context = new CarrerasBDEntities())
            {
                Jugadores jugador = new Jugadores()
                {
                    NombreJugador = jugadorDto.Nombre,
                    Saldo = Convert.ToDecimal(20000)
                };

                context.Jugadores.Add(jugador);

                await context.SaveChangesAsync();

                return jugador;
            }
        }

        public async void ActualizarSaldoApuestaJugador(int idJugador, decimal saldoResta)
        {
            using(CarrerasBDEntities context = new CarrerasBDEntities())
            {
                var jugador = context.Jugadores.FirstOrDefault(a => a.IdJugador == idJugador);

                jugador.Saldo -= saldoResta;

                await context.SaveChangesAsync();
            }
        }

        public async Task<decimal> ActualizarSaldoGanadorJugador(int idJugador, decimal saldoSuma)
        {
            using (CarrerasBDEntities context = new CarrerasBDEntities())
            {
                var jugador = context.Jugadores.FirstOrDefault(a => a.IdJugador == idJugador);

                jugador.Saldo += saldoSuma;

                await context.SaveChangesAsync();

                return jugador.Saldo;
            }
        }

        public async Task<List<Jugadores>> ObtenerParticipantes(List<JugadorDTO> jugadores)
        {
            List<Jugadores> jugadoresRetorno = new List<Jugadores>();

            using(CarrerasBDEntities context = new CarrerasBDEntities())
            {
                foreach (var item in jugadores)
                {
                    var jugadorParticipante = await context.Jugadores.FirstOrDefaultAsync(a => a.IdJugador == item.IdJugador);

                    if(jugadorParticipante != null)
                    {
                        jugadoresRetorno.Add(jugadorParticipante);
                    }
                }
            }

            return jugadoresRetorno;
        }
    }
}