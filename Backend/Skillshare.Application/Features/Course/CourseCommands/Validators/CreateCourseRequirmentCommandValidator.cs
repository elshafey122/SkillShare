using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.Features.Course.CourseCommands.Models;
using Skillshare.Contracts.DTOs.CourseDTOs;

namespace Skillshare.Application.Features.Course.CourseCommands.Validators
{
    public class CreateCourseRequirmentCommandValidator : AbstractValidator<CoursePrerequisiteDTO>
    {
        public CreateCourseRequirmentCommandValidator()
        {
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("{ProperyName} is required");
        }
    }
}