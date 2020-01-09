using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class GetAllBooksForCategory
    {
        public class Query : IRequest<List<Book>>
        {
            public int CategoryId { get; set; }
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
                var books = await _context.BookCategories.Where(bc => bc.CategoryId == request.CategoryId)
                    .Select(bc => bc.Book)
                    .ToListAsync();

                return books;
            }
        }
    }
}
