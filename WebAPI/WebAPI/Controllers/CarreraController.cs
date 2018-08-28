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
    public class CarreraController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IHttpActionResult> Crear([FromBody] CarreraDTO carrera)
        {
            return Ok(await new CarreraRepository().CrearCarrera(carrera));
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<IHttpActionResult> ObtenerCarreras()
        {
            return Ok(await new CarreraRepository().ObtenerCarreras());
        }
    }
}
