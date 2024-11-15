using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.ResponseHandler;
using Skillshare.Contracts.DTOs.CourseLangugeDTOs;

namespace Skillshare.Application.Features.CourseCategory.CourseCategoryQuries.Models
{
    public record GetLangugesModelQuery() : IRequest<ResponseModel<List<CourselangugeDTO>>>;
}