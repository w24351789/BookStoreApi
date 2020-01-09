using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CountryApp
{
    public class GetCountry
    {
        public class Query : IRequest<Review>
        {
            public int CountryId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Review>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Review> Handle(Query request, CancellationToken cancellationToken)
            {
                var country = await _context.Countries.FindAsync(request.CountryId);

                return country;
            }
        }
    }
}
