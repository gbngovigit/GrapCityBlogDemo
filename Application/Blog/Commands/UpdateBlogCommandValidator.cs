using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Blog.Commands
{
   
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {

        public UpdateBlogCommandValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Value is not valid");
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
            RuleFor(v => v.Abstract)
              .NotEmpty().WithMessage("Abstract is required.")
              .MaximumLength(250).WithMessage("Abstract must not exceed 200 characters.");
            RuleFor(v => v.Contents)
              .NotEmpty().WithMessage("Title is required.");
              
        }
    }
}
