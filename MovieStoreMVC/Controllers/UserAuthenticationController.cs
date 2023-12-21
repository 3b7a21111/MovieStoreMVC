using Microsoft.AspNetCore.Mvc;
using MovieStoreMVC.Models.DTO;
using MovieStoreMVC.Repository.Abstract;

namespace MovieStoreMVC.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService authService;

        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }
        //create one admin in our project 
        //public async Task<IActionResult> Register()
        //{
        //    var model = new RegisterModel
        //    {
        //        Email = "admin@gmail.com",
        //        userName = "admin",
        //        Name = "Muhammed",
        //        Password = "Admin@123",
        //        PasswordConfirm = "Admin@123",
        //        Role = "Admin"
        //    };
        //    var result = await authService.RegisterAsync(model);
        //    return Ok(result.Message);
        //}

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login ( LoginModel model)
        {
            if (!ModelState.IsValid)       
                return View(model);
     
            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            
            else
            {
                TempData["msg"] = "Could not logged in";
                return RedirectToAction(nameof(Login));
            }
        }
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
