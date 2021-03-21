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

    public class CreateBlogCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Contents { get; set; }

        public class CreateQuoteCommandHandler : IRequestHandler<CreateBlogCommand, string>
        {
            private readonly IApplicationDbContext _context;


            public CreateQuoteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {

                var entity = new Article()
                {
                    Title = request.Title,
                    Abstract = request.Abstract,
                    Contents = request.Contents
                };
             
                _context.Articles.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return null;
            }
        }

    }
}
