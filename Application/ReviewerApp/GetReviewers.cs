using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewerApp
{
    public class GetReviewers
    {
        public class Query : IRequest<List<Reviewer>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Reviewer>>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<List<Reviewer>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reviewers = await _context.Reviewers.ToListAsync();

                return reviewers;
            }
        }

    }
}
