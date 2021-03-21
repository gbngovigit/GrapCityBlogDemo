using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Blog.Queries
{
    public class GetBlogQuery : IRequest<ArticleVm>
    {
        public int Id { get; set; }
        public class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, ArticleVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetBlogQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ArticleVm> Handle(GetBlogQuery request, CancellationToken cancellationToken)
            {
                var result = await _context.Articles.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (result != null)
                    return _mapper.Map<ArticleVm>(result);

                else
                    return null;
            }
        }
    }

}
