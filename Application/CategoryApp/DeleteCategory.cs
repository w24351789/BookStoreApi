﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class DeleteCategory
    {
        public class Command : IRequest
        {
            public int CategoryId { get; set; }
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
                var categories = await _context.Categories.ToListAsync();

                var delCategory = await _context.Categories.FindAsync(request.CategoryId);

                var categoryHaveBooks = _context.BookCategories.Where(bc => bc.CategoryId == request.CategoryId)
                    .Select(bc => bc.Book)
                    .ToList()
                    .Count();

                if (categoryHaveBooks > 0)
                    throw new Exception($"Cannot delete because {delCategory} has books");

                foreach(var c in categories)
                {
                    if (c.Name.Trim().ToUpper() == delCategory.Name.Trim().ToUpper())
                        throw new Exception($"Category {delCategory} already existed");
                }

                _context.Remove(delCategory);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problem saving change");
            }
        }
    }
}
