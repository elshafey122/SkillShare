﻿using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Application.Features.CourseLecture.CourseLectureCommand.Models;
using Skillshare.Application.ResponseHandler;
using Skillshare.Application.Shared;
using Skillshare.Contracts.ServicesContracts;

namespace Skillshare.Application.Features.CourseLecture.CourseLectureCommand.handlers
{
    internal class LectureCommandhandler : ResponseHandlerModel,
        IRequestHandler<CreateLectureModelCommand, ResponseModel<bool>>,
        IRequestHandler<UpdateLectureModelCommand, ResponseModel<bool>>,
        IRequestHandler<DeleteLectureModelCommand, ResponseModel<bool>>

    {
        private readonly ICourseLectureService _CourseLectureService;

        public LectureCommandhandler(IStringLocalizer<Sharedresources> stringLocalizer, ICourseLectureService courseLectureService) : base(stringLocalizer)
        {
            _CourseLectureService = courseLectureService;
        }

        public async Task<ResponseModel<bool>> Handle(CreateLectureModelCommand request, CancellationToken cancellationToken)
        {
            var Response = await _CourseLectureService.CreateLecture(request.SectionId);
            if (!Response)
            {
                return BadRequest<bool>();
            }
            return Success(Response);
        }

        public async Task<ResponseModel<bool>> Handle(UpdateLectureModelCommand request, CancellationToken cancellationToken)
        {
            var Response = await _CourseLectureService.UpdateLecture(request.Lecture);
            if (!Response)
            {
                return BadRequest<bool>();
            }
            return Success(Response);
        }

        public async Task<ResponseModel<bool>> Handle(DeleteLectureModelCommand request, CancellationToken cancellationToken)
        {
            var IsDeleted = await _CourseLectureService.DeleteLecture(request.LectureId);
            if (!IsDeleted)
            {
                return BadRequest<bool>();
            }
            return Success(IsDeleted);
        }
    }
}