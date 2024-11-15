using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.LectureDTOs;

namespace Skillshare.Application.Features.CourseLecture.CourseLectureCommand.Models
{
    public record UpdateLectureModelCommand(LectureForUpdateDTO Lecture) : IRequest<ResponseModel<bool>>;
}