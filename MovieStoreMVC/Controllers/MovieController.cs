using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Repository.Abstract;

namespace MovieStoreMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IFileService fileService;
        private readonly IGenreService genreService;

        public MovieController(IMovieService movieService,IFileService fileService,IGenreService genreService)
        {
            this.movieService = movieService;
            this.fileService = fileService;
            this.genreService = genreService;
        }
        public IActionResult Add()
        {
            var movie = new Movie();
            movie.GenreList = genreService.GetAll().Select(x => new SelectListItem
            {
                Text=x.GenreName,
                Value=x.Id.ToString()
            });
            return View(movie);
        }
        [HttpPost]
        public IActionResult Add (Movie movie)
        {
            movie.GenreList = genreService.GetAll().Select(x => new SelectListItem
            {
                Text = x.GenreName,
                Value = x.Id.ToString()
            });
            //check image enterd correct [path]
            if (movie.ImageFile != null)
            {
                var FileResult = fileService.SaveImage(movie.ImageFile!);
                if (FileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not save";
                    return View(movie);
                }
                var imageName = FileResult.Item2;
                movie.MovieImage = imageName;
            }
            //movie.GenreNames = "ay";
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            
            var result = movieService.Add(movie);
            if (result)
            {
                TempData["msg"] = "Your Movie is added successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Can not add your Movie";
                return View(movie);
            }
        }


        public IActionResult Update(int id)
        {
            var model = movieService.GetById(id);
            var allgenrebymovie = movieService.GenresBYmovie(model.Id);
            MultiSelectList genrebymovie = new MultiSelectList(genreService.GetAll(), "Id", "GenreName",allgenrebymovie);
            model.GenreByMovie = genrebymovie;
            return View(model);
        }
        [HttpPost]
        public IActionResult Update (Movie movie)
        {
            var allgenrebymovie = movieService.GenresBYmovie(movie.Id);
            MultiSelectList genrebymovie = new MultiSelectList( genreService.GetAll(), "Id", "GenreName", allgenrebymovie);
            movie.GenreByMovie = genrebymovie;
            if (movie.ImageFile != null)
            {
                var FileResult = fileService.SaveImage(movie.ImageFile!);
                if (FileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not save";
                    return View(movie);
                }
                var imageName = FileResult.Item2;
                movie.MovieImage = imageName;
            }
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            var result = movieService.Update(movie);
            if (result)
            {
                //TempData["msg"] = "Your Movie is added successfully";
                return RedirectToAction(nameof(GetAll));
            }
            else
            {
                TempData["msg"] = "Can not add your Movie";
                return View(movie);
            }
        }
        public IActionResult Delete (int id)
        {
            movieService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }
        public IActionResult GetAll()
        {
            var data = movieService.GetAll();
            return View(data);
        }
    }
}
