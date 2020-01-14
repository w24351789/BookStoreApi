using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookApp
{
    public class GetBookRating
    {
        public class Query : IRequest<decimal>
        {
            public int BookId { get; set; }
        }

        public class Handler : IRequestHandler<Query, decimal>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<decimal> Handle(Query request, CancellationToken cancellationToken)
            {
                var reviews = _context.Reviews.Where(r => r.Book.Id == request.BookId)
                                              .Select(r => r.Rating);

                var rating = (decimal)reviews.Sum() / reviews.Count();

                return await Task.FromResult(rating);
            }
        }
    }
}
