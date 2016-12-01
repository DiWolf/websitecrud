using System.Linq;
using System.Threading.Tasks;
using elguero.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elguero.Controllers
{
    public class HomeController : Controller
    {
        private readonly GueroModel _contexto;
        
        public HomeController (GueroModel contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Menu() 
        {

            return View(await _contexto.menu.ToListAsync());
        }

        public  async Task<IActionResult> Historia()
        {
            var h = await _contexto.historia.SingleAsync();
            return View(h); 
        }

        public async Task<IActionResult> Promociones() 
        {
            return View(await _contexto.promo.ToListAsync());
        }

        public async Task<IActionResult> Sucursal (int id)
        {
            if(id==null)
            {
                return NotFound(); 
            }
            var sucursal = await _contexto.sucursal.SingleAsync(s=>s.Id == id);
            if(sucursal == null)
            {
                return NotFound();
            }
            return View(sucursal);
        }

        public IActionResult Contacto()
        {
           

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
