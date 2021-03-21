using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Blog.Queries
{
    public class ArticleVm : IMapFrom<Article>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Contents { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Article, ArticleVm>();
        }
    }
}
