using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Models.DTO;

namespace MovieStoreMVC.Repository.Abstract
{
    public interface IMovieService
    {
        bool Add(Movie movie);
        bool Update(Movie movie);
        bool Delete(int id);
        MovieListVm GetAll(string term="", bool paging = false, int CurrentPage = 0);
        Movie GetById(int id);
        List<int> GenresBYmovie(int movieId);
    }
}
