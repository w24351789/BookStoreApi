using Application.Errors;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewApp
{
    public class EditReview
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Headline { get; set; }
            public string ReviewText { get; set; }
            public int Rating { get; set; }
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
                var review = await _context.Reviews.FindAsync(request.Id);
                if (review == null) throw new RestException(HttpStatusCode.NotFound, new { Review = $"{review} not found" });

                review.Headline = request.Headline ?? review.Headline;
                review.ReviewText = request.ReviewText ?? review.ReviewText;
                review.Rating = request.Rating;

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving change");

            }
        }
    }
}
