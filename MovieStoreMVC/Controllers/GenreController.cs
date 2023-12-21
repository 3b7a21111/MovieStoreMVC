using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Repository.Abstract;

namespace MovieStoreMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View(genre);
            }
            var result= genreService.Add(genre);
            if (result)
            {
                TempData["msg"] = "Your Movie Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Could not Add your Movie";
                return View(genre);
            }
        }
        public IActionResult Update(int id)
        {
            var genre = genreService.GetById(id);
            return View(genre);
        }
        [HttpPost]
        public IActionResult Update (Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View(genre);
            }
            var result = genreService.Update(genre);
            if (result)
            {
                //TempData["msg"] = "Your Movie is Updated Successfullt";
                return RedirectToAction(nameof(GetAll));
            }
            else
            {
                TempData["msg"] = "Can not update your Movie";
                return View(genre);
            }
        }
        public IActionResult GetAll()
        {
            var Genres = genreService.GetAll().ToList();
            return View(Genres);
        }
        public IActionResult Delete (int id)
        {

            var result = genreService.Delete(id);
             return RedirectToAction(nameof(GetAll));
            
      

        }
    }
}
