using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class GetCategory
    {
        public class Query : IRequest<Category>
        {
            public int CategoryId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Category>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Category> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FindAsync(request.CategoryId);
                return category;
            }
        }
    }
}
