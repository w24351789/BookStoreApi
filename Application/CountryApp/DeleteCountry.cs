using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CountryApp
{
    public class DeleteCountry
    {
        public class Command : IRequest
        {
            public int CountryId { get; set; }
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

                var delCountry = await _context.Countries.FindAsync(request.CountryId);

                if (delCountry == null)
                    throw new Exception("Country not found");

                var countryHasAuthor = _context.Authors.Where(a => a.Country.Id == request.CountryId).Count() > 0;
                if (countryHasAuthor)
                    throw new Exception($"{delCountry} has author cannot delete.");

                _context.Remove(delCountry);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem deleting Country");
            }
        }
    }
}
