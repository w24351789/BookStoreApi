using Application.Errors;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class EditCategory
    {
        public class Command : IRequest
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
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
                var category = await _context.Categories.FindAsync(request.CategoryId);

                if (category == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Category = "Category not found" });

                category.Name = request.Name ?? category.Name;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving change");
            }
        }
    }
}
