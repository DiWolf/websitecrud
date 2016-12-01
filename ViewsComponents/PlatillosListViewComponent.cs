using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elguero.Entities.Eventos;
using elguero.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elguero.ViewsComponents 
{
    public class PlatillosListViewComponent : ViewComponent
    {
        private readonly GueroModel _contexto; 
        public PlatillosListViewComponent(GueroModel contexto)
        {
            _contexto = contexto; 
        }

        public  IViewComponentResult Invoke()
        {
           
            var platillo = _contexto.eventplatillos.ToList();
            return View(platillo);
            
        }

         private Task<List<Platillos>> GetItemsAsync()
        {
            return _contexto.eventplatillos.ToListAsync();
            
        }    
    }
}