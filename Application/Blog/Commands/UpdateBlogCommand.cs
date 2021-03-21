using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Blog.Commands
{
    public class UpdateBlogCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Contents { get; set; }

        public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, int>
        {
            private readonly IApplicationDbContext _context;


            public UpdateBlogCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
            {

                var entity = await _context.Articles.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Article), request.Id);
                }

                entity.Title = request.Title;
                entity.Contents = request.Contents;
                entity.Abstract = request.Abstract;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }

    }

}
