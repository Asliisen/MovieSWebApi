using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        public int ActorId;
        public readonly IMovieStoreDbContext _context;

        public readonly IMapper _mapper;

        public GetActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
            {
                _context = context;

                _mapper = mapper;
            }

        public ActorDetailViewModel Handle()
        {
            // Find the actor with the specified ID.
            var actor = _context.Actors.SingleOrDefault(x => x.Id == ActorId);
            if (actor is null)
            {
                throw new InvalidOperationException("Actor Does Not Exist!");

            }

            // Map the actor entity to the view model.
            return _mapper.Map<ActorDetailViewModel>(actor);
        }
    }

    public class ActorDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
        }
}
