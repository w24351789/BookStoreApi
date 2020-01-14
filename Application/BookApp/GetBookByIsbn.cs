using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookApp
{
    public class GetBookByIsbn
    {
        public class Query : IRequest<Book>
        {
            public string BookIsbn { get; set; }
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
                var book =  _context.Books.Where(b => b.Isbn == request.BookIsbn).FirstOrDefault();

                return await Task.FromResult(book);
            }
        }



    }
}