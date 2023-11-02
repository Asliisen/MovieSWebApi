using System.Linq;
using System;
using AutoMapper;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Applications.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        public CreateOrderModel Model { get; set; }

        private readonly IMovieStoreDbContext _context;

        private readonly IMapper _mapper;

        public CreateOrderCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        public void Handle()
        {
            // Check if an order with the same buyer and movie already exists.
            var order = _context.Orders.SingleOrDefault(x => x.User.Id == Model.BuyerId && x.Movie.Id == Model.MovieId);

            if (order is not null)
                 throw new InvalidOperationException("Order Is  Already Exist");

            // Map the CreateOrderModel to an Order entity.
            order = _mapper.Map<Order>(Model);

            // Find the user and movie associated with the order.
            order.User = _context.Users.SingleOrDefault(x => x.Id == Model.BuyerId);

            order.Movie = _context.Movies.SingleOrDefault(x => x.Id == Model.MovieId);

            // Set the MovieGenreId to the genre of the ordered movie.
            order.MovieGenreId = order.Movie.GenreId;

            // Add the order to the context and save changes.
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public class CreateOrderModel
            {
                public int MovieId { get; set; }
                public int BuyerId { get; set; }
                public DateTime BuyDate { get; set; } = DateTime.Now.Date;
            }
    }
}
