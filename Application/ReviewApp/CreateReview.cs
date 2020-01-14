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

namespace Application.ReviewApp
{
    public class CreateReview
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Headline { get; set; }
            public string ReviewText { get; set; }
            public int Rating { get; set; }
            public Reviewer Reviewer { get; set; }
            public Book Book { get; set; }
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
                var reviewReviewer = await _context.Reviewers.FindAsync(request.Reviewer.Id);
                if (reviewReviewer == null)
                    throw new RestException(HttpStatusCode.Forbidden, new { Review = "Cannot add review, reviewer not found" });
                var reviewBook = await _context.Books.FindAsync(request.Book.Id);
                if (reviewBook == null)
                    throw new RestException(HttpStatusCode.Forbidden, new { Review = "Cannot add review, book not found" });

                var review = new Review
                {
                    Id = request.Id,
                    Headline = request.Headline,
                    ReviewText = request.ReviewText,
                    Rating = request.Rating,
                    Reviewer = reviewReviewer,
                    Book = reviewBook
                };

                _context.Reviews.Add(review);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving change");
            }
        }
    }
}
