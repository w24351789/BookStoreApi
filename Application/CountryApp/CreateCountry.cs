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

namespace Application.CountryApp
{
    public class CreateCountry
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public virtual ICollection<Author> Authors { get; set; }
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
                
                foreach(var c in _context.Countries)
                {
                    if (c.Name.Trim().ToUpper() == request.Name.Trim().ToUpper())
                    {
                        throw new RestException(HttpStatusCode.Forbidden, new { Country = "Country already exist" });
                    }
                }
                
                var country = new Country
                {
                    Id = request.Id,
                    Name = request.Name
                };

                _context.Countries.Add(country);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving country");
            }
        }
    }
}
