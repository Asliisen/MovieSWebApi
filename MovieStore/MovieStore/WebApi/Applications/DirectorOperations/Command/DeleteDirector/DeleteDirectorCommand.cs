using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }

        private readonly IMovieStoreDbContext _context;

            public DeleteDirectorCommand(IMovieStoreDbContext context)
            {
                _context = context;
            }

        public void Handle()
        {
            // Find the director with the specified ID.
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Director Does not Found.");

            // Check if any movies in the system have this director.
            var movies = _context.Movies.Where(x => x.DirectorId == DirectorId);
            if (movies is not null)
                throw new InvalidOperationException("A Movie in the System has Director!");

            // If the director is not associated with any movies, remove the director from the database.
            _context.Directors.Remove(director);
            _context.SaveChanges();
        }
    }


}
