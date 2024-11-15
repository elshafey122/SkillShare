using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Contracts.DTOs.CartItem;

namespace Skillshare.Application.Features.Cart.CartCommand.Validator
{
    public class CartForCreateValidator : AbstractValidator<CartItemForCreate>
    {
        public CartForCreateValidator()
        {
            RuleFor(c => c.courseId).NotEmpty().WithMessage("{PropertyName} is not empty");
            RuleFor(c => c.courseId).NotNull().WithMessage("{PropertyName} is not null");

            RuleFor(c => c.price).NotNull().WithMessage("{PropertyName} is not null");

            RuleFor(c => c.userId).NotEmpty().WithMessage("{PropertyName} is not empty");
            RuleFor(c => c.userId).NotNull().WithMessage("{PropertyName} is not null");
        }
    }
}