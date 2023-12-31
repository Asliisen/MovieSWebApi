using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Webapi.TokenOperations;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
            private readonly IMovieStoreDbContext _context;
            public CreateTokenModel Model { get; set; }
            private readonly IMapper _mapper;

            private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMovieStoreDbContext context,IMapper mapper,IConfiguration configuration)
        {
            _context = context;

            _mapper = mapper;

            _configuration = configuration;
        }

        public Token Handle()
        {
            // Find the user with the specified email and password.
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if (user is not null)
            {
                // Create an access token for the user.
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                // Update the user's refresh token and expiration date.
                user.RefreshToken = token.Refreshtoken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("User Mail and Password are wrong!");
            }
        }

            public class CreateTokenModel
            {
                public string Email { get; set; }
                public string Password { get; set; }
            }
    }
}
