using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public CreateActorModel Model { get; set; }

        public CreateActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var existingActor = _context.Actors
                .FirstOrDefault(x => x.Name == Model.Name && x.LastName == Model.LastName);

            if (existingActor != null)
            {
                throw new InvalidOperationException("Actor already exists.");
            }

            var newActor = new Actor
            {
                Name = Model.Name,
                LastName = Model.LastName
            };

            _context.Actors.Add(newActor);
            _context.SaveChanges();
        }
    }

    public class CreateActorModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
