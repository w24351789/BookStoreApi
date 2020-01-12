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

namespace Application.ReviewerApp
{
    public class GetReviewsByReviewer
    {
        public class Query : IRequest<List<Review>>
        {
            public int ReviewerId { get; set; }
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
                var reviews = await _context.Reviews.Where(r => r.Reviewer.Id == request.ReviewerId)
                    .ToListAsync();

                return reviews;
            }
        }
    }
}
