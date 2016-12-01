using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace elguero.Entities.Account
{
    public class MyIdentityRole : IdentityRole
    {
        public string Description {get;set;}
    }
}