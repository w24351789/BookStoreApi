using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewerApp
{
    public class GetReviewerOfAReview
    {
        public class Query : IRequest<Reviewer>
        {
            public int ReviewId { get; set; }
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
                var reviewer = _context.Reviews.Where(r => r.Id == request.ReviewId)
                    .Select(r => r.Reviewer)
                    .FirstOrDefault();

                return await Task.FromResult(reviewer);
                    
            }
        }
    }
}
