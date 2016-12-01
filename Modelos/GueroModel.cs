using elguero.Entities;
using elguero.Entities.Account;
using elguero.Entities.Administrator;
using elguero.Entities.Eventos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace elguero.Modelos
{
    public class GueroModel : IdentityDbContext<MyIdentityUser,MyIdentityRole,string>
    {
        public GueroModel(DbContextOptions<GueroModel> options)
        :base(options){}

        public DbSet<Historia> historia{get;set;}
        public DbSet<Menu> menu {get;set;}
        public DbSet<Promocion> promo {get;set;}
        public DbSet<Sucursal> sucursal {get;set;}

        public DbSet<Evento> eventopage {get;set;}
        public DbSet<EventoGal> eventosgal{get;set;}
        public DbSet<Galeria> GaleriasEventos {get;set;}
        public DbSet<MenuEventos> menueventos {get;set;}
        public DbSet<Platillos> eventplatillos {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entitdad Historia
            var h = modelBuilder.Entity<Historia>();
            h.Property(p =>p.Entrada).HasColumnType("text");
            h.Property(p =>p.Texto).HasColumnType("text");

            //Entidad Eventos
            var eventos = modelBuilder.Entity<Evento>(); 
            eventos.Property(eve=>eve.Contenido).HasColumnType("text");
            eventos.Property(eve=>eve.Pie).HasColumnType("text");
            eventos.Property(eve=>eve.Titulo).HasColumnType("text");

            //Entidad Sucursales
            var su = modelBuilder.Entity<Sucursal>(); 
            su.Property(s=>s.GoogleMaps).HasColumnType("text");
            su.Property(s=>s.Servicios).HasColumnType("text");
            su.Property(s=>s.SucursalNombre).HasColumnType("text");
            su.Property(s=>s.Ubicacion).HasColumnType("text");

            //Salon Privado

            base.OnModelCreating(modelBuilder);
        }
    }
}