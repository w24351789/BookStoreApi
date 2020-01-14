using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AuthorApp
{
    public class GetBooksOfAnAuthor
    {
        public class Query : IRequest<List<Book>>
        {
            public int AuthorId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Book>>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<List<Book>> Handle(Query request, CancellationToken cancellationToken)
            {
                var books = await _context.BookAuthors.Where(ba => ba.AuthorId == request.AuthorId)
                                                      .Select(ba => ba.Book)
                                                      .ToListAsync();

                return books;
            }
        }
    }
}
