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

namespace Application.AuthorApp
{
    public class GetAuthor
    {
        public class Query : IRequest<Author>
        {
            public int AuthorId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Author>
        {
            private readonly BookDbContext _context;

            public Handler(BookDbContext context)
            {
                _context = context;
            }

            public async Task<Author> Handle(Query request, CancellationToken cancellationToken)
            {
                var author =  _context.Authors.Where(a => a.Id == request.AuthorId)
                    .FirstOrDefault();
                    
                if (author == null)
                    throw new Exception("Not Found");

                return await Task.FromResult(author);
            }
        }
    }
}
