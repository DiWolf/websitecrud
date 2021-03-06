using elguero.Entities;
using elguero.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace elguero.Controllers
{
   public class AccountController : Controller
{
 private readonly UserManager<MyIdentityUser> userManager;
 private readonly SignInManager<MyIdentityUser> loginManager;
 private readonly RoleManager<MyIdentityRole> roleManager;


 public AccountController(UserManager<MyIdentityUser> userManager,
    SignInManager<MyIdentityUser> loginManager,
    RoleManager<MyIdentityRole> roleManager)
 {
   this.userManager = userManager;
   this.loginManager = loginManager;
   this.roleManager = roleManager;
 }

     public IActionResult Register()
    {
    return View();
    }

    [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Register(RegisterViewModel obj)
{
    if (ModelState.IsValid)
    {
        MyIdentityUser user = new MyIdentityUser();
        user.UserName = obj.UserName;
        user.Email = obj.Email;
        user.NombreCompleto = obj.FullName;
        user.Cumpleanios = obj.BirthDate;

        IdentityResult result = userManager.CreateAsync
        (user, obj.Password).Result;

        if (result.Succeeded)
        {
            if(!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                MyIdentityRole role = new MyIdentityRole();
                role.Name = "NormalUser";
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
                if(!roleResult.Succeeded)
                {
                    ModelState.AddModelError("", 
                     "Error while creating role!");
                    return View(obj);
                }
            }

            userManager.AddToRoleAsync(user, 
                         "NormalUser").Wait();
            return RedirectToAction("Login", "Account");
        }
    }
    return View(obj);

}
public IActionResult Login()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Login(LoginViewModel obj)
{
    if (ModelState.IsValid)
    {
        var result = loginManager.PasswordSignInAsync
        (obj.UserName, obj.Password, 
          obj.RememberMe,false).Result;

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Admin");
        }

        ModelState.AddModelError("", "Invalid login!"); 
    }

    return View(obj);
}
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult LogOff()
{
    loginManager.SignOutAsync().Wait();
    return RedirectToAction("Login","Account");
}
}
}
