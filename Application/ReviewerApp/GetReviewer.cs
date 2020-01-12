using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewerApp
{
    public class GetReviewer
    {
        public class Query : IRequest<Reviewer>
        {
            public int ReviewerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Reviewer>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Reviewer> Handle(Query request, CancellationToken cancellationToken)
            {
                var reviewer = await _context.Reviewers.FindAsync(request.ReviewerId);

                return reviewer;
            }
        }
    }
}
