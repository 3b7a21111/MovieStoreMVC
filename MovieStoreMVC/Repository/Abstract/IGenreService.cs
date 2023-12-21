using MovieStoreMVC.Models.Domain;

namespace MovieStoreMVC.Repository.Abstract
{
    public interface IGenreService
    {
        bool Add(Genre genre);
        bool Update(Genre genre);
        bool Delete(int id);
       IQueryable<Genre> GetAll();
       Genre GetById (int id);
    }
}
