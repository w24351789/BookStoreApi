using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BookApp
{
    public class GetBook
    {
        public class Query : IRequest<Book>
        {
            public int BookId { get; set; }
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
                var book = await _context.Books.FindAsync(request.BookId);
                return book;
            }
        }



    }
}
