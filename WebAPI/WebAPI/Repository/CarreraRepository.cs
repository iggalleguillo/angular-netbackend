using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace WebAPI.Repository
{
    public class CarreraRepository
    {
        //se aplicara logica de negocio en este componente debido a lo acotado.
        public async Task<CarreraDTO> CrearCarrera(CarreraDTO carreraDto)
        {
            Random random = new Random();
            using (CarrerasBDEntities context = new CarrerasBDEntities())
            {
                Carreras carrera = new Carreras();
                carrera.CaballoGanador = await ObtenerGanador();
                context.Carreras.Add(carrera);
                await context.SaveChangesAsync();

                carrera.NombreCarrera = string.Format("Carrera {0}", carrera.IdCarrera);
                carreraDto.Participantes = await AsignarPorcentaje(carreraDto.Participantes);
                await InsertarDetalleCarrera(carrera, carreraDto.Participantes);
                
                    carrera.CarreraJugador = context.CarreraJugador.Where(a => a.Carreras.IdCarrera == carrera.IdCarrera).ToList();
                
                await context.SaveChangesAsync();
                return await RetornoCarrera(carreraDto, carrera);
            }
        }

        private async Task<bool> AsignarSaldoGanador(Carreras carrera)
        {
            bool retorno = false;
            foreach (var ganador in carrera.CarreraJugador)
            {
                var caballoGanador = await new CaballoRepository().ObtenerCaballoPorId(carrera.CaballoGanador);
                if (ganador.CaballoApostado.Equals(carrera.CaballoGanador))
                {
                    var cantidadMasBonificador = ganador.CantidadApostada * caballoGanador.Bonificador;
                    ganador.Jugadores.Saldo = await new JugadorRepository().ActualizarSaldoGanadorJugador(ganador.Jugadores.IdJugador, cantidadMasBonificador);
                    retorno = true;
                }
            }

            return retorno;
        }

        private async Task<CarreraDTO> RetornoCarrera(CarreraDTO carreraDto, Carreras carrera)
        {
            carreraDto.Participantes.Clear();
            JugadorDTO jugadorDto = null;

            foreach (var jugador in carrera.CarreraJugador)
            {
                jugadorDto = new JugadorDTO()
                {
                    IdJugador = jugador.Jugadores.IdJugador,
                    CaballoApostado = jugador.CaballoApostado,
                    Nombre = jugador.Jugadores.NombreJugador,
                    Saldo = jugador.Jugadores.Saldo,
                    CantidadApostada = jugador.CantidadApostada
                };
                carreraDto.Participantes.Add(jugadorDto);
            }

            await AsignarSaldoGanador(carrera);

            foreach (var jugador in carrera.CarreraJugador)
            {
                jugadorDto = new JugadorDTO()
                {
                    IdJugador = jugador.Jugadores.IdJugador,
                    CaballoApostado = jugador.CaballoApostado,
                    Nombre = jugador.Jugadores.NombreJugador,
                    Saldo = jugador.Jugadores.Saldo,
                    CantidadApostada = jugador.CantidadApostada
                };

                if (jugador.CaballoApostado == carrera.CaballoGanador)
                {
                    carreraDto.Ganadores.Add(jugadorDto);
                }
            }

            carreraDto.IdCarrera = carrera.IdCarrera;
            carreraDto.CaballoGanador = await new CaballoRepository().ObtenerCaballoPorId(carrera.CaballoGanador);
            carreraDto.NombreCarrera = carrera.NombreCarrera;

            return carreraDto;
        }

        private async Task<bool> InsertarDetalleCarrera(Carreras carrera, List<JugadorDTO> participantes)
        {
            Random random = new Random();
            CarreraJugador carreraJugador = null;
            decimal saldoApostado = 0;
            List<Jugadores> participantesCarrera = await new JugadorRepository().ObtenerParticipantes(participantes);
            using (CarrerasBDEntities context = new CarrerasBDEntities())
            {
                var valorApostado = 0;
                string tipoApuesta = TipoApuesta();
                
                foreach (var jugadorCarrera in participantesCarrera)
                {
                    switch (tipoApuesta)
                    {
                        case "CONSERVADORA":
                            valorApostado = random.Next(10, 21);
                            break;
                        default:
                            valorApostado = random.Next(5, 16);
                            break;
                    }
                    if (jugadorCarrera.Saldo > 0)
                    {
                        if (jugadorCarrera.Saldo >= 2000)
                        {
                            saldoApostado = (jugadorCarrera.Saldo / 100) * valorApostado;
                            carreraJugador = new CarreraJugador()
                            {
                                Jugador = jugadorCarrera.IdJugador,
                                CaballoApostado = participantes.FirstOrDefault(a => a.IdJugador == jugadorCarrera.IdJugador).CaballoApostado,
                                Carrera = carrera.IdCarrera,
                                CantidadApostada = saldoApostado
                            };
                        }
                        else
                        {
                            if (jugadorCarrera.Saldo < 2000)
                            {
                                saldoApostado = jugadorCarrera.Saldo;
                                carreraJugador = new CarreraJugador()
                                {
                                    Jugador = jugadorCarrera.IdJugador,
                                    CaballoApostado = participantes.FirstOrDefault(a => a.IdJugador == jugadorCarrera.IdJugador).CaballoApostado,
                                    Carrera = carrera.IdCarrera,
                                    CantidadApostada = saldoApostado
                                };
                            }
                        }
                        new JugadorRepository().ActualizarSaldoApuestaJugador(jugadorCarrera.IdJugador, saldoApostado);
                        context.CarreraJugador.Add(carreraJugador);
                    }
                }

                return await context.SaveChangesAsync() > 0 ? true : false;
            }
        }

        private async Task<int> ObtenerGanador()
        {
            Random random = new Random();
            int ganador = random.Next(1, 101);
            List<Caballos> caballos = await new CaballoRepository().ObtenerCaballos();
            Caballos caballoGanador = new Caballos();

            foreach (var caballo in caballos)
            {
                if(ganador >= caballo.ProbApuestaInicial && ganador <= caballo.ProbApuestaFinal)
                {
                    caballoGanador = caballo;
                }
            }

            return caballoGanador.IdCaballo;
        }

        private async Task<List<JugadorDTO>> AsignarPorcentaje(List<JugadorDTO> listaParticipantes)
        {
            List<Caballos> caballos = await new CaballoRepository().ObtenerCaballos();
            Random random = new Random();

            foreach (var participante in listaParticipantes)
            {
                var porcentaje = random.Next(1, 101);
                foreach (var caballo in caballos)
                {
                    if(porcentaje >= caballo.ProbApuestaInicial && porcentaje <= caballo.ProbApuestaFinal)
                    {
                        participante.CaballoApostado = caballo.IdCaballo;
                    }
                }
            }

            return listaParticipantes;
        }

        private string TipoApuesta()
        {
            string url = "https://free.currencyconverterapi.com/api/v6/";
            string parametros = "convert?q=EUR_USD&compact=ultra&date={0}&endDate={1}";
            string desde = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string hasta = DateTime.Now.ToString("yyyy-MM-dd");
            string retorno = string.Empty;

            decimal valorAyer = 0;
            decimal valorHoy = 0;
            parametros = parametros.Replace("{0}", desde);
            parametros = parametros.Replace("{1}", hasta);
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(url);

            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
                ("application/json"));

            HttpResponseMessage response = http.GetAsync(parametros).Result;

            if (response.IsSuccessStatusCode)
            {
                var datosRespuesta = response.Content.ReadAsAsync<Object>().Result;

                string[] datos = datosRespuesta.ToString().Split(':');

                valorAyer = Convert.ToDecimal(datos[2].Split(',')[0].Replace('.', ','));
                valorHoy = Convert.ToDecimal(datos[3].Replace('}', ' ').Replace('.', ','));

                retorno = valorHoy > valorAyer ? "CONSERVADORA" : "NORMAL";
            }

            return retorno;
        }

        public async Task<List<CarreraDTO>> ObtenerCarreras()
        {
            List<CarreraDTO> listaCarreras = new List<CarreraDTO>();
            using (CarrerasBDEntities contexto = new CarrerasBDEntities())
            {
                CarreraDTO carreraDto = null;
                JugadorDTO jugadorDto = null;
                var carreras = contexto.Carreras.ToList();

                foreach (var carrera in carreras)
                {
                    carreraDto = new CarreraDTO();
                    carreraDto.IdCarrera = carrera.IdCarrera;
                    carreraDto.NombreCarrera = carrera.NombreCarrera;

                    foreach (var jugador in carrera.CarreraJugador)
                    {
                        jugadorDto = new JugadorDTO()
                        {
                            IdJugador = jugador.Jugadores.IdJugador,
                            CaballoApostado = jugador.CaballoApostado,
                            Nombre = jugador.Jugadores.NombreJugador,
                            Saldo = jugador.Jugadores.Saldo,
                            CantidadApostada = jugador.CantidadApostada
                        };
                        carreraDto.Participantes.Add(jugadorDto);
                    }

                    carreraDto.CaballoGanador = await new CaballoRepository().ObtenerCaballoPorId(carrera.CaballoGanador);
                    listaCarreras.Add(carreraDto);
                }
            }

            return listaCarreras;
        }
    }
}