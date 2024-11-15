using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.CourseSection.CourseSectionCommand.Models
{
    public record DeleteSectionModelCommand(int SectionId) : IRequest<ResponseModel<bool>>;
}