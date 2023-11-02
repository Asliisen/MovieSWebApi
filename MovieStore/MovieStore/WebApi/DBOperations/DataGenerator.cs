using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                    return;

                context.Genres.AddRange(
                    new Genre { Name = "Action" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Fantasy" },
                    new Genre { Name = "Horror" },
                    new Genre { Name = "Mystery" },
                    new Genre { Name = "Romance" });
                context.SaveChanges();

                context.Directors.AddRange(
                    new Director { Name = "Director1Name", LastName = "Director1LastName" },
                    new Director { Name = "Director2Name", LastName = "Director2LastName" },
                    new Director { Name = "Director3Name", LastName = "Director3LastName" },
                    new Director { Name = "Director4Name", LastName = "Director4LastName" },
                    new Director { Name = "Director5Name", LastName = "Director5LastName" },
                    new Director { Name = "Director6Name", LastName = "Director6LastName" });
                context.SaveChanges();

                context.Actors.AddRange(
                    new Actor { Name = "Actor1Name", LastName = "Actor1LastName" },
                    new Actor { Name = "Actor2Name", LastName = "Actor2LastName" },
                    new Actor { Name = "Actor3Name", LastName = "Actor3LastName" },
                    new Actor { Name = "Actor4Name", LastName = "Actor4LastName" },
                    new Actor { Name = "Actor5Name", LastName = "Actor5LastName" },
                    new Actor { Name = "Actor6Name", LastName = "Actor6LastName" },
                    new Actor { Name = "Actor7Name", LastName = "Actor7LastName" },
                    new Actor { Name = "Actor8Name", LastName = "Actor8LastName" },
                    new Actor { Name = "Actor9Name", LastName = "Actor9LastName" },
                    new Actor { Name = "Actor10Name", LastName = "Actor10LastName" },
                    new Actor { Name = "Actor11Name", LastName = "Actor11LastName" },
                    new Actor { Name = "Actor12Name", LastName = "Actor12LastName" },
                    new Actor { Name = "Actor13Name", LastName = "Actor13LastName" },
                    new Actor { Name = "Actor14Name", LastName = "Actor14LastName" });
                context.SaveChanges();

                context.Movies.AddRange(
                    new Movie
                    {
                        Name = "Movie1",
                        GenreId = 1,
                        DirectorId = 1,
                        Price = 30,
                        PublishDate = new DateTime(2000, 1, 1),
                        Actors = new List<Actor>()
                    },
                    new Movie
                    {
                        Name = "Movie2",
                        GenreId = 2,
                        DirectorId = 2,
                        Price = 20,
                        PublishDate = new DateTime(2000, 1, 2),
                        Actors = new List<Actor>()
                    },
                    new Movie
                    {
                        Name = "Movie3",
                        GenreId = 3,
                        DirectorId = 3,
                        Price = 10,
                        PublishDate = new DateTime(2000, 1, 3),
                        Actors = new List<Actor>()
                    },
                    new Movie
                    {
                        Name = "Movie4",
                        GenreId = 4,
                        DirectorId = 4,
                        Price = 40,
                        PublishDate = new DateTime(2000, 1, 4),
                        Actors = new List<Actor>()
                    },
                    new Movie
                    {
                        Name = "Movie5",
                        GenreId = 5,
                        DirectorId = 5,
                        Price = 25,
                        PublishDate = new DateTime(2000, 1, 5),
                        Actors = new List<Actor>()
                    },
                    new Movie
                    {
                        Name = "Movie6",
                        GenreId = 6,
                        DirectorId = 6,
                        Price = 15,
                        PublishDate = new DateTime(2000, 1, 6),
                        Actors = new List<Actor>()
                    });
                context.SaveChanges();

                context.Users.AddRange(
                    new User
                    {
                        Name = "User1Name",
                        LastName = "User1LastName",
                        Email = "user1@hotmail.com",
                        Password = "user1password",
                        Movies = new List<Movie>(),
                        Genres = new List<Genre>()
                    },
                    new User
                    {
                        Name = "User2Name",
                        LastName = "User2LastName",
                        Email = "user2@hotmail.com",
                        Password = "user2password",
                        Movies = new List<Movie>(),
                        Genres = new List<Genre>()
                    },
                    new User
                    {
                        Name = "User3Name",
                        LastName = "User3LastName",
                        Email = "user3@hotmail.com",
                        Password = "user3password",
                        Movies = new List<Movie>(),
                        Genres = new List<Genre>()
                    });
                context.SaveChanges();
            }
        }
    }
}
