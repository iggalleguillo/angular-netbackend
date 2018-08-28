using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class CaballoRepository
    {
        public async Task<List<Caballos>> ObtenerCaballos()
        {
            using (CarrerasBDEntities contexto = new CarrerasBDEntities())
            {
                return await contexto.Caballos.ToListAsync();
            }
        }

        public async Task<CaballoDTO> ObtenerCaballoPorId(int? idCaballo)
        {
            CaballoDTO caballoDto = null;

            using (CarrerasBDEntities contexto = new CarrerasBDEntities())
            {
                var caballo = await contexto.Caballos.FirstOrDefaultAsync(a => a.IdCaballo == idCaballo);

                caballoDto = new CaballoDTO()
                {
                    IdCaballo = caballo.IdCaballo,
                    NombreCaballo = caballo.NombreCaballo,
                    Bonificador = caballo.Bonificador
                };

                return caballoDto;
            }
        }
    }
}