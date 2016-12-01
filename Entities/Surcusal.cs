using System.ComponentModel.DataAnnotations;

namespace elguero.Entities
{
    public class Sucursal 
    {
        public int Id {get;set;}
        
        public string  GoogleMaps {get;set;}
        public string SucursalNombre {get;set;}
        public string Ubicacion {get;set;}
        public string Servicios {get;set;}

    }
}