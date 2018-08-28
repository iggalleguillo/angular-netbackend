using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    public class JugadorController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IHttpActionResult> Crear([FromBody] JugadorDTO jugador)
        {
            //para no utilizar instancias de esta manera es mejor utilizar injeccion de dependencias
            //sumando a un modelo singleton, pero dado que para una webapi de asp.net se necesita 
            //incluir mayores implementaciones para este caso no se utilizará.
            //En asp.net core el modelo singleton e injeccion de dependencias viene integrado en 
            //su forma de trabajo por lo que es facil, rapido y muy recomendable utilizarlo.
            return Ok(await new JugadorRepository().CrearJugador(jugador));
        }
    }
}
