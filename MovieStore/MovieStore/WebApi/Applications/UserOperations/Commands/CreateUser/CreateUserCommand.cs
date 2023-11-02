using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
            public CreateUserModel Model { get; set; }

            private readonly IMovieStoreDbContext _context;

            private readonly IMapper _mapper;

        public CreateUserCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        public void Handle()
        {
            // Check if a user with the same email already exists.
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user is not null)
                throw new InvalidOperationException("User allready Exist.");

            // Map the CreateUserModel to a User entity.
            user = _mapper.Map<User>(Model);

            // Add the user to the context and save changes.
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<Genre> FavoriteGenres { get; set; }
            public List<Movie> PurchasedMovies { get; set; }
        }
}
