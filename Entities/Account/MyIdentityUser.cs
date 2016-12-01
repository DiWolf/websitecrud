using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace elguero.Entities.Account 
{
    public class MyIdentityUser : IdentityUser
    {
        public string NombreCompleto {get;set;}
        public DateTime Cumpleanios {get;set;}
    }
}