using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Repository.Abstract;

namespace MovieStoreMVC.Repository.Implementation
{
    public class GenreService :IGenreService
    {
        private readonly DataBaseContext _context;
        public GenreService(DataBaseContext context)
        {   
            _context = context;
        }
        public bool Add(Genre genre) 
        {
            try
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool Update(Genre genre)
        {
            try
            {
                _context.Genres.Update(genre);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var deletedGenre = _context.Genres.Find(id);
                if(deletedGenre == null)
                {
                    return false;
                }
                _context.Genres.Remove(deletedGenre);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public IQueryable<Genre> GetAll()
        {
            return _context.Genres.AsQueryable();
        }
        public Genre GetById(int id)
        {
            // Check if the ID exists in the database
            bool idExists = _context.Genres.Any(g => g.Id == id);

            // If the ID exists, retrieve and return the record; otherwise, return null or handle accordingly
            return idExists ? _context.Genres.Find(id)! : null!;
        }
    }
}
