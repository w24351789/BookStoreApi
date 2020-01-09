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
    public class GetCountryOfAnAuthor
    {
        public class Query : IRequest<Review>
        {
            public int AuthorId { get; set; }
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
                var country = _context.Authors.Where(a => a.Id == request.AuthorId)
                    .Select(a => a.Country)
                    .FirstOrDefault();

                return await Task.FromResult(country);
            }
        }
    }
}
