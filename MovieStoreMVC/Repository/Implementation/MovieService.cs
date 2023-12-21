using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Models.DTO;
using MovieStoreMVC.Repository.Abstract;

namespace MovieStoreMVC.Repository.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly DataBaseContext context;

        public MovieService(DataBaseContext context)
        {
            this.context = context;
        }

        public bool Add(Movie movie)
        {
            try
            {
                context.Movies.Add(movie);
                context.SaveChanges();
                foreach (var genreId in movie.Genres)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId=movie.Id,
                        GenreId=genreId
                    };
                    context.MovieGenres.Add(movieGenre);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var deletedMovie = GetById(id);
                if(deletedMovie == null)
                {
                    return false;
                }
                var movieGenres = context.MovieGenres.Where(x => x.MovieId == deletedMovie.Id);
                foreach (var MovieGenre in movieGenres)
                {
                    context.MovieGenres.Remove(MovieGenre);
                }
                context.Movies.Remove(deletedMovie);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                    return false;
            }
        }

        public Movie GetById(int id)
        {
            return context.Movies.Find(id);
        }

        public MovieListVm GetAll(string term = "", bool paging = false , int CurrentPage=0)
        {
            var data = new MovieListVm();
            var list = context.Movies.ToList();
            if(! string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(x => x.Title.ToLower().StartsWith(term)).ToList();
            }
            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int totalpages = (int) Math.Ceiling(count/(double)pageSize);
                list =list.Skip((CurrentPage-1)*pageSize).Take(pageSize).ToList();
                data.TotalPages = totalpages;
                data.PageSize = pageSize;
                data.CurrentPage = CurrentPage;
            }
            foreach (var movie in list)
            {
                var genres = (from genre in context.Genres join mg in context.MovieGenres
                                   on genre.Id equals mg.GenreId
                                  where mg.MovieId == movie.Id
                                  select genre.GenreName
                    ).ToList();
                var genrenames = string.Join(',', genres);
                movie.GenreNames = genrenames;
            }
            data.MovieList = list.AsQueryable();
            return data;
        }

        public bool Update(Movie movie)
        {
            try
            {
                var genredeleted = context.MovieGenres.Where(x=>x.MovieId==movie.Id && !movie.Genres.Contains(x.GenreId)).ToList();
                foreach (var moviegenre in genredeleted)
                {
                    context.MovieGenres.Remove(moviegenre);
                }
                context.Movies.Update(movie);
                foreach (int genre in movie.Genres)
                {
                    var moviegenre = context.MovieGenres.FirstOrDefault(x => x.MovieId == movie.Id && x.GenreId == genre);
                    if (moviegenre == null)
                    {
                        moviegenre = new MovieGenre
                        {
                            MovieId=movie.Id,
                            GenreId=genre
                        };
                        context.MovieGenres.Add(moviegenre);
                    }
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GenresBYmovie(int movieId)
        {
            var genreIds = context.MovieGenres.Where(x => x.MovieId == movieId).Select(x => x.GenreId).ToList();
            return genreIds;
        }
    }
}
