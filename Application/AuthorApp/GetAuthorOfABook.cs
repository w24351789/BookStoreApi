using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AuthorApp
{
    public class GetAuthorOfABook
    {
        public class Query : IRequest<Author>
        {
            public int BookId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Author>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Author> Handle(Query request, CancellationToken cancellationToken)
            {
                var author =  _context.BookAuthors.Where(ba => ba.BookId == request.BookId)
                                                  .Select(ba => ba.Author)
                                                  .FirstOrDefault();

                return await Task.FromResult(author);
            }
        }
    }
}
