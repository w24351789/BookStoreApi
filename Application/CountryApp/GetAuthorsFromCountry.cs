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

namespace Application.CountryApp
{
    public class GetAuthorsFromCountry
    {
        public class Query : IRequest<List<Author>>
        {
            public int CountryId { get; set; }
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
                var authors = await _context.Authors.Where(c => c.Id == request.CountryId)
                    .ToListAsync();

                return authors;
            }
        }
    }
}
