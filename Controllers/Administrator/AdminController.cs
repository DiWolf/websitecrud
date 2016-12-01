using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using elguero.Entities;
using elguero.Entities.Account;
using elguero.Entities.Administrator;
using elguero.Entities.Eventos;
using elguero.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elguero.Controllers.Administrator
{
    public class AdminController : Controller
    {
        private readonly GueroModel _contexto;
        private readonly UserManager<MyIdentityUser> userManager;
         private IHostingEnvironment _environment;
        public AdminController(GueroModel contexto, IHostingEnvironment environment, UserManager<MyIdentityUser> 
                      userManager)
        {
             _environment = environment;
            _contexto  = contexto;
             this.userManager = userManager;
        }
        [Authorize(Roles="NormalUser")]        
        public IActionResult Index() 
        {
            MyIdentityUser user = userManager.GetUserAsync
                         (HttpContext.User).Result;
                        
            return View(); 
            
        }
        [Authorize(Roles="NormalUser")]  
        //Aparatdo de histora 
          public async Task<IActionResult> HistoriaAdmin()
        {
            var historia = await _contexto.historia.ToListAsync(); 
            return View(historia);
        }
        [Authorize(Roles="NormalUser")]  
        public async Task<IActionResult> EditarHistoria(int id) 
        {
             if(id==null)
             {
                 return NotFound(); 
             }   
            var h = await _contexto.historia.SingleOrDefaultAsync(hi =>hi.Id == id);
            if(h==null)
            {
                return NotFound(); 
            }
            return View(h);
        }
        [Authorize(Roles="NormalUser")]  
        [HttpPost]
          [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult>EditarHistoria(Historia h)
        {
            if(h.Id==0)
            {
                return NotFound(); 
            }
            if(ModelState.IsValid)
            {
                    try{
                        _contexto.Update(h);
                        await _contexto.SaveChangesAsync();
                    }catch(Exception ex)
                    {
                        return RedirectToAction("Index");
                    }
                   
            }
             return View(h);
        }
        [Authorize(Roles="NormalUser")]  
        public async Task<IActionResult> Sucursales()
        {
            return View(await _contexto.sucursal.ToListAsync());
        }

        //Crear nueva sucursal 
        [Authorize(Roles="NormalUser")]  
        public IActionResult CrearSucursal() 
        {
            return View(); 
        }
        [Authorize(Roles="NormalUser")]  

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> CrearSucursal(Sucursal s) 
        {
            
            if(ModelState.IsValid)
            {
                _contexto.sucursal.Add(s);
                 await _contexto.SaveChangesAsync(); 
                //return RedirectToAction("Index");
            }
            return View(s);
        }
        [Authorize(Roles="NormalUser")]  
        public async Task<IActionResult> EditarSucursal(int id) 
        {
             if(id==null)
             {
                 return NotFound(); 
             }   
            var h = await _contexto.sucursal.SingleOrDefaultAsync(hi =>hi.Id == id);
            if(h==null)
            {
                return NotFound(); 
            }
            return View(h);
        }
        [Authorize(Roles="NormalUser")]  
         [HttpPost]
          [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult>EditarSucursal(Sucursal s) 
        {
            if(s.Id==0)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try{
                    _contexto.Update(s);
                    await _contexto.SaveChangesAsync();
                }catch(Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(s);
        }
        [Authorize(Roles="NormalUser")]  
        public async Task<IActionResult> EventosInfo()
        {
            return View(await _contexto.eventopage.ToListAsync());
        } 

         public IActionResult CrearEventos() 
        {
            return View(); 
        }

        [Authorize(Roles="NormalUser")]  
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> CrearEventos(Evento e) 
        {
            
            if(ModelState.IsValid)
            {
                _contexto.eventopage.Add(e);
                 await _contexto.SaveChangesAsync(); 
                //return RedirectToAction("Index");
            }
            return View(e);
        }
        [Authorize(Roles="NormalUser")]  
         public async Task<IActionResult> EditarEventos(int id) 
        {
             if(id==null)
             {
                 return NotFound(); 
             }   
            var h = await _contexto.eventopage.SingleOrDefaultAsync(hi =>hi.Id == id);
            if(h==null)
            {
                return NotFound(); 
            }
            return View(h);
        }
        [Authorize(Roles="NormalUser")]  
         [HttpPost]
         [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> EditarEventos(Evento even)
        {
            if(even.Id == 0)
            {
                return NotFound(); 
            }
            if(ModelState.IsValid)
            {
                try{
                    _contexto.Update(even);
                    await _contexto.SaveChangesAsync(); 
                }catch(Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(even);
        }
#region  
//Galeria de Eventos    
    [Authorize(Roles="NormalUser")]  
        public async Task<IActionResult> GaleriaEventos()
        {
            return View(await _contexto.eventosgal.ToListAsync());
        } 
        [Authorize(Roles="NormalUser")]  
         public IActionResult CrearGaleriaEventos() 
        {
            return View(); 
        }
        [Authorize(Roles="NormalUser")]  

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> CrearGaleriaEventos(EventoGal e, ICollection<IFormFile> files) 
        {
            var upload = Path.Combine(_environment.WebRootPath, "images");
             
             foreach (var file in files)
        {
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(upload, file.FileName), FileMode.Create))
                {
                    e.RutaFotoF = "/images/"+file.FileName;
                    await file.CopyToAsync(fileStream);
                }
               
            
            }
        }
         if(ModelState.IsValid)
            {
               
                _contexto.eventosgal.Add(e);
                 await _contexto.SaveChangesAsync(); 
                //return RedirectToAction("Index");
            }
        return View(e);
            
        }
        [Authorize(Roles="NormalUser")]  
         public async Task<IActionResult> EditarGaleriaEventos(int id) 
        {
             if(id==null)
             {
                 return NotFound(); 
             }   
            var h = await _contexto.eventosgal.SingleOrDefaultAsync(hi =>hi.Id == id);
            if(h==null)
            {
                return NotFound(); 
            }
            return View(h);
        }
        [Authorize(Roles="NormalUser")]  
         [HttpPost]
         [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> EditarGaleriaEventos(EventoGal even)
        {
            if(even.Id == 0)
            {
                return NotFound(); 
            }
            if(ModelState.IsValid)
            {
                try{
                    _contexto.Update(even);
                    await _contexto.SaveChangesAsync(); 
                }catch(Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(even);
        }
#endregion

//Menejar Promociones
#region
    [Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> MostrarPromociones()
    {
        return View(await _contexto.promo.ToListAsync());
    }
    [Authorize(Roles="NormalUser")]  
    public IActionResult CrearPromociones() 
    {
        return View(); 
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> CrearPromociones(Promocion pro, ICollection<IFormFile> files)
    {
        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    pro.RutaImage = "/images/"+file.FileName; 
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        if(ModelState.IsValid)
        {
            _contexto.promo.Add(pro);
            await _contexto.SaveChangesAsync(); 
        }

        return View(pro);
    }
    [Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> EditarPromociones(int id)
    {
        if (id == null)
        {
            return NotFound(); 
        }
        var p = await _contexto.promo.SingleOrDefaultAsync(x=>x.Id== id);
        if(p==null)
        {
            return NotFound(); 
        }
        return View(p);
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> EditarPromociones(Promocion p, ICollection<IFormFile> files)
    {
        if(p.Id== 0)
        {
            return NotFound(); 
        }

        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    
                    p.RutaImage = "/images/"+file.FileName; 
                    await file.CopyToAsync(fileStream);
                    
                }
            }
        }

        if(ModelState.IsValid)
        {
            try{
                _contexto.Update(p);
                await _contexto.SaveChangesAsync(); 
            }catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        return View(p);
    }
    /*
MostrarPromociones
EditarPromociones
CrearPromociones
EliminarPromociones
**/
#endregion

#region
    [Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> MostrarPlatillos()
    {
        return View(await _contexto.eventplatillos.ToListAsync());
    }
    [Authorize(Roles="NormalUser")]  
    public IActionResult CrearPlatillos() 
    {
        return View(); 
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> CrearPlatillos(Platillos pro, ICollection<IFormFile> files)
    {
        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    pro.RutaImage = "/images/"+file.FileName; 
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        if(ModelState.IsValid)
        {
            _contexto.eventplatillos.Add(pro);
            await _contexto.SaveChangesAsync(); 
        }

        return View(pro);
    }
    [Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> EditarPlatillos(int Id)
    {
        if (Id == null)
        {
            return NotFound(); 
        }
        var p = await _contexto.eventplatillos.SingleOrDefaultAsync(x=>x.id== Id);
        if(p==null)
        {
            return NotFound(); 
        }
        return View(p);
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> EditarPlatillos(Platillos p, ICollection<IFormFile> files)
    {
        if(p.id== 0)
        {
            return NotFound(); 
        }

        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    
                    p.RutaImage = "/images/"+file.FileName; 
                    await file.CopyToAsync(fileStream);
                    
                }
            }
        }

        if(ModelState.IsValid)
        {
            try{
                _contexto.Update(p);
                await _contexto.SaveChangesAsync(); 
            }catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        return View(p);
    }
 /*
MostrarPlatillos
CrearPlatillos
EditarPlatillos
EliminarPlatillos
    */
#endregion

#region

[Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> MostrarMenuRestaurante()
    {
        return View(await _contexto.menu.ToListAsync());
    }
    [Authorize(Roles="NormalUser")]  
    public IActionResult CrearMenuRestaurante() 
    {
        return View(); 
    }

    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> CrearMenuRestaurante(Menu pro, ICollection<IFormFile> files)
    {
        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    pro.Ruta1 = "/images/"+file.FileName; 
                    pro.Ruta2 = "/images/"+file.FileName;
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        if(ModelState.IsValid)
        {
            _contexto.menu.Add(pro);
            await _contexto.SaveChangesAsync(); 
        }

        return View(pro);
    }
    [Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> EditarMenuRestaurante(int id)
    {
        if (id == null)
        {
            return NotFound(); 
        }
        var p = await _contexto.menu.SingleOrDefaultAsync(x=>x.Id== id);
        if(p==null)
        {
            return NotFound(); 
        }
        return View(p);
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> EditarMenuRestaurante(Menu p, ICollection<IFormFile> files)
    {
        if(p.Id== 0)
        {
            return NotFound(); 
        }

        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    
                    p.Ruta1 = "/images/"+file.FileName; 
                    p.Ruta2 = "/images/"+file.FileName;
                    await file.CopyToAsync(fileStream);
                    
                }
            }
        }

        if(ModelState.IsValid)
        {
            try{
                _contexto.Update(p);
                await _contexto.SaveChangesAsync(); 
            }catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        return View(p);
    }
/*
    MostrarMenuRestaurante
    CrearMenuRestaurante
    EditarMenuRestaurante
**/
#endregion

#region
    [Authorize(Roles="NormalUser")]  
    public async Task<IActionResult> MostrarMenuEventos()
    {
        return View(await _contexto.menueventos.ToListAsync());
    }
    [Authorize(Roles="NormalUser")]  
    public IActionResult CrearMenuEventos() 
    {
        return View(); 
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> CrearMenuEventos(MenuEventos pro, ICollection<IFormFile> files)
    {
        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    pro.RutaImage = "/images/"+file.FileName; 
                    pro.RutaImage2 = "/images/"+file.FileName;
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        if(ModelState.IsValid)
        {
            _contexto.menueventos.Add(pro);
            await _contexto.SaveChangesAsync(); 
        }

        return View(pro);
    }

  [Authorize(Roles="NormalUser")]  

    public async Task<IActionResult> EditarMenuEventos(int id)
    {
        if (id == null)
        {
            return NotFound(); 
        }
        var p = await _contexto.menueventos.SingleOrDefaultAsync(x=>x.Id== id);
        if(p==null)
        {
            return NotFound(); 
        }
        return View(p);
    }
    [Authorize(Roles="NormalUser")]  
    [HttpPost]
    [ValidateAntiForgeryTokenAttribute]
    public async Task<IActionResult> EditarMenuEventos(MenuEventos p, ICollection<IFormFile> files)
    {
        if(p.Id== 0)
        {
            return NotFound(); 
        }

        var upload = Path.Combine(_environment.WebRootPath, "images");
        foreach(var file in files)
        {
            if(file.Length>0)
            {
                using(var fileStream = new FileStream(Path.Combine(upload, file.FileName),FileMode.Create))
                {
                    
                    p.RutaImage = "/images/"+file.FileName; 
                     p.RutaImage2 = "/images/"+file.FileName;
                    await file.CopyToAsync(fileStream);
                    
                }
            }
        }

    
        if(ModelState.IsValid)
        {
            try{
                _contexto.Update(p);
                await _contexto.SaveChangesAsync(); 
            }catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        return View(p);
    }
    /*

    */
#endregion
      /*
MostrarMenuEventos
CrearMenuEventos
EditarMenuEventos
*/

    }
}