using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Route("api/tourist/problem")]
    [Authorize(Policy = "touristPolicy")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly INotificationService _notificationService;
        private readonly ITourService _tourService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService, INotificationService notificationService)
        {
            _tourProblemService = tourProblemService;
            _notificationService = notificationService;
            _tourService = tourService;
        }

        [HttpPost("create")]
        public ActionResult<PagedResult<TourProblemDto>> Create([FromBody] TourProblemDto tourProblemDto)
        {
            int userId = User.PersonId();
            tourProblemDto = setProblem(tourProblemDto, userId);

            var result = _tourProblemService.Create(tourProblemDto);
            notifyCreatedReport(result.Value);
            return CreateResponse(result);
        }

        private TourProblemDto setProblem(TourProblemDto tourProblemDto, int userId)
        {
            tourProblemDto.TouristId = userId;
            tourProblemDto.Status = ProblemStatus.Pending;
            tourProblemDto.Comments = null;
            tourProblemDto.Details.Time = DateTime.UtcNow;
            return tourProblemDto;
        }

        private void notifyCreatedReport(TourProblemDto tourProblemDto)
        {
            string tourName = _tourService.GetById(tourProblemDto.TourId).Value.Name;
            int tourAuthorId = _tourService.GetById(tourProblemDto.TourId).Value.AuthorId;
            string content = $"You have a new report for tour {tourName}!";
            _notificationService.Create(new NotificationDto(content, NotificationType.TourProblemComment, tourProblemDto.Id, tourAuthorId, false));
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll()
        {
            var results = _tourProblemService.GetAll();
            return CreateResponse(results);
        }

        [HttpGet("byId")]
        public ActionResult<PagedResult<TourProblemDto>> GetById([FromQuery] int id)
        {
            var result = _tourProblemService.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet("getByTouristId")]
        public ActionResult<PagedResult<TourProblemDto>> GetByTouristId()
        {
            int userId = User.PersonId();

            var result = _tourProblemService.GetByTouristId(userId);
            
            return CreateResponse(result);
        }

        [HttpPost("addComment")]
        public ActionResult<PagedResult<TourProblemDto>> AddComment([FromQuery] int tourProblemId, ProblemCommentDto commentDto)
        {
            var result = _tourProblemService.AddComment(tourProblemId, commentDto);
            notifyAddedComment(_tourProblemService.GetById(tourProblemId).Value);
            return CreateResponse(result);
        }

        private void notifyAddedComment(TourProblemDto tourProblemDto)
        {
            string tourName = _tourService.GetById(tourProblemDto.TourId).Value.Name;
            int tourAuthorId = _tourService.GetById(tourProblemDto.TourId).Value.AuthorId;
            string content = $"You have a new comment on report of a tour {tourName}!";
            _notificationService.Create(new NotificationDto(content, NotificationType.TourProblemComment, tourProblemDto.Id, tourAuthorId, false));
        }

        [HttpPut("changeStatus")]
        public ActionResult<PagedResult<TourProblemDto>> ChangeStatus([FromQuery] int tourProblemId, ProblemStatus problemStatus)
        {
            var result = _tourProblemService.ChangeStatus(tourProblemId, problemStatus);
            notifyChangedStatus(result.Value);
            return CreateResponse(result);
        }

        private void notifyChangedStatus(TourProblemDto tourProblemDto)
        {
            string tourName = _tourService.GetById(tourProblemDto.TourId).Value.Name;
            int tourAuthorId = _tourService.GetById(tourProblemDto.TourId).Value.AuthorId;
            var status = tourProblemDto.Status;
            string content = $"Changed status for a report of a tour {tourName} to {status}!";
            _notificationService.Create(new NotificationDto(content, NotificationType.TourProblemComment, tourProblemDto.Id, tourAuthorId, false));
        }
    }
}
