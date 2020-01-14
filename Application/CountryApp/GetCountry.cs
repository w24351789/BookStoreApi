using Application.Errors;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CountryApp
{
    public class GetCountry
    {
        public class Query : IRequest<Country>
        {
            public int CountryId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Country>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Country> Handle(Query request, CancellationToken cancellationToken)
            {
                var country = await _context.Countries.FindAsync(request.CountryId);
                if (country == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Country = $"{country} did not exist in Country list." });

                return country;
            }
        }
    }
}
