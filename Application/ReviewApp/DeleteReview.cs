using Application.Errors;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewApp
{
    public class DeleteReview
    {
        public class Command : IRequest
        {
            public int ReviewId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var delReview = await _context.Reviews.FindAsync(request.ReviewId);
                if (delReview == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Review = $"{delReview} not found." });

                _context.Reviews.Remove(delReview);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving change");

            }
        }
    }
}
