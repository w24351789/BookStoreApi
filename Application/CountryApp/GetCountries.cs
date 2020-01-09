using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CountryApp
{
    public class GetCountries
    {
        public class Query : IRequest<List<Review>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Review>>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<List<Review>> Handle(Query request, CancellationToken cancellationToken)
            {
                var countries = await _context.Countries.ToListAsync();

                return countries;
            }
        }
    }
    

}
