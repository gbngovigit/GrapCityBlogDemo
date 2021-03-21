using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Blog.Commands
{
 

    public class DeleteBlogCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class CreateQuoteCommandHandler : IRequestHandler<DeleteBlogCommand, int>
        {
            private readonly IApplicationDbContext _context;


            public CreateQuoteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
            {

                var entity = await _context.Articles
                   .Where(l => l.Id == request.Id)
                   .SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Article), request.Id);
                }

                _context.Articles.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }

    }
}
