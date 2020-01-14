using Application.Errors;
using Domain;
using MediatR;
using Persistence;
using System.Net;
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
                if (reviewer == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Reviewer = "Reviewer not found" });
                return reviewer;
            }
        }
    }
}
