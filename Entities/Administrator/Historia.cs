using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace elguero.Entities.Administrator
{
    public class Historia
    {
        public int Id {get;set;}
        
        public string Entrada {get;set;}
        
        public string Texto {get;set;}
    }
}