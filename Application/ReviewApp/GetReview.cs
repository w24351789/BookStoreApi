﻿using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReviewApp
{
    public class GetReview
    {
        public class Query : IRequest<Review>
        {
            public int ReviewId { get; set; }
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
                var review = await _context.Reviews.FindAsync(request.ReviewId);
                return review;
            }
        }



    }
}
