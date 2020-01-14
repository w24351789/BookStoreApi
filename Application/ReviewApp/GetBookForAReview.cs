using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewApp
{
    public class GetBookForAReview
    {
        public class Query : IRequest<Book>
        {
            public int ReviewId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Book>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
            {
                var review = _context.Reviews.Where(r => r.Id == request.ReviewId)
                                             .Select(r => r.Book)
                                             .FirstOrDefault();

                return await Task.FromResult(review);

            }
        }
    }
}
