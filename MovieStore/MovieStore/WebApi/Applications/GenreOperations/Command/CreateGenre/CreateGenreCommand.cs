using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
            public CreateGenreModel Model { get; set; }

            private readonly IMovieStoreDbContext _context;

            public CreateGenreCommand(IMovieStoreDbContext context)
                {
                    _context = context;
                }

        public void Handle()
        {
            // Check if a genre with the same name already exists.
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if (genre is not null)
                throw new InvalidOperationException("Movie Type Is Already Exist!!");

            // If the genre doesn't exist, create a new genre and add it to the database.
            genre = new Genre();

            genre.Name = Model.Name;

            _context.Genres.Add(genre);

            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
        {
            public string Name { get; set; }
        }
}
