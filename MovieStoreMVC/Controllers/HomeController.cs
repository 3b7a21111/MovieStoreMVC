using Microsoft.AspNetCore.Mvc;
using MovieStoreMVC.Repository.Abstract;

namespace MovieStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService movieService;

        public HomeController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        public IActionResult Index(string term="",int CurrentPage = 0)
        {
            var movies = movieService.GetAll(term,true,CurrentPage);
            return View(movies);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult MovieDetail (int movieId)
        {
            var movie = movieService.GetById(movieId);
            return View(movie);
        }
    }
}
