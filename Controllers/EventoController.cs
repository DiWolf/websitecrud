using System.Linq;
using System.Threading.Tasks;
using elguero.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elguero.Controllers
{
    public class EventoController: Controller
    {
         private readonly GueroModel _contexto;

         public EventoController(GueroModel contexto)
         {
             _contexto= contexto;
         }

         public IActionResult IndexEvento() 
         {
            return View();
         }
         public  async Task<IActionResult> Index()
        {
            var evento = await _contexto.eventopage.SingleAsync();
            return View(evento); 
        }

        public async Task<IActionResult> MenuEventos()
        {
            var menu = await _contexto.menueventos.ToListAsync(); 
            return View(menu);
        }

        public async Task<IActionResult> Galeria() 
        {
            var galeria = await _contexto.eventosgal.ToListAsync(); 
            return View(galeria);
        }

        public async Task<IActionResult> GaleriaDateails (int id)
        {
            if(id==null)
            {
                return NotFound(); 
            }
            var sucursal = await _contexto.GaleriasEventos.Where(s=>s.IdGaleria == id).ToListAsync();
            if(sucursal == null)
            {
                return NotFound();
            }
            return View(sucursal);
        }
        

    }
}