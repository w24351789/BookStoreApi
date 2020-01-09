using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AuthorApp
{
    public class GetAuthors
    {
        public class Query : IRequest<List<Author>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Author>>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<List<Author>> Handle(Query request, CancellationToken cancellationToken)
            {
                var authors = await _context.Authors.ToListAsync();

                return authors;
            }
        }
    }
}
