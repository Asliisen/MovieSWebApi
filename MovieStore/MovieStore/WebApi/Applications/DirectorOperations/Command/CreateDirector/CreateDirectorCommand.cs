using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;

        public CreateDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            // Check if a director with the same name and last name already exists.
            var director = _context.Directors.SingleOrDefault(x => x.Name == Model.Name && x.LastName == Model.LastName);

            if (director is not null)
                throw new InvalidOperationException("Director Already Exist.");

            // If the director doesn't exist, create a new director and add them to the database.
            director = new Director();
            director.Name = Model.Name;
            director.LastName = Model.LastName;

            _context.Directors.Add(director);
            _context.SaveChanges();
        }
    }

        public class CreateDirectorModel
        {
            public string Name { get; set; }
            public string LastName { get; set; }
        }
}
