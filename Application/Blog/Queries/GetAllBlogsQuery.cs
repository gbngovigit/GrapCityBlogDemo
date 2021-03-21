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
    public class GetAllBlogsQuery : IRequest<List<ArticleVm>>
    {
        public class GetAllBlogsHandler : IRequestHandler<GetAllBlogsQuery, List<ArticleVm>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllBlogsHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ArticleVm>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
            {
              
                var reuslt = await _context.Articles.ToListAsync(cancellationToken);

                return _mapper.Map<List<ArticleVm>>(reuslt.ToList());
               
            }
        }
    }

}
