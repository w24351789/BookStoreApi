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
    public class GetAllCategoriesOfABook
    {
        public class Query : IRequest<List<Category>>
        {
            public int BookId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Category>>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<List<Category>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await  _context.BookCategories.Where(bc => bc.BookId == request.BookId)
                    .Select(bc => bc.Category)
                    .ToListAsync();

                return categories;
            }
        }
    }
}
