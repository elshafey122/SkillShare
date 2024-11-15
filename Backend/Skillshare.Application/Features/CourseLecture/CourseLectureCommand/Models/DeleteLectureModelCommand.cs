using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;

namespace Skillshare.Application.Features.CourseLecture.CourseLectureCommand.Models
{
    public record DeleteLectureModelCommand(int LectureId) : IRequest<ResponseModel<bool>>;
}