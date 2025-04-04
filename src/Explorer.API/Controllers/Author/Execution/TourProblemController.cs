using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Execution
{
    [Route("api/author/problem")]
    [Authorize(Policy = "authorPolicy")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly INotificationService _notificationService;
        private readonly ITourService _tourService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService, INotificationService notificationService)
        {
            _tourProblemService = tourProblemService;
            _tourService = tourService;
            _notificationService = notificationService;
        }

        [HttpGet("getByAuthorId")]
        public ActionResult<PagedResult<TourProblemDto>> GetByAuthorId()
        {
            int userId = User.PersonId();

            var tours = _tourService.GetByAuthorId(0, 0, userId);
            var tourIds = tours.Value.Results.Select(tour => tour.Id).ToList();
            var results = _tourProblemService.GetByToursIds(tourIds);
            return CreateResponse(results);
        }

        [HttpPost("addComment")]
        public ActionResult<PagedResult<TourProblemDto>> AddComment([FromQuery] int tourProblemId, ProblemCommentDto commentDto)
        {
            var result = _tourProblemService.AddComment(tourProblemId, commentDto);
            notifyAddedComment(result.Value);
            return CreateResponse(result);
        }

        private void notifyAddedComment(TourProblemDto tourProblemDto)
        {
            string tourName = _tourService.GetById(tourProblemDto.TourId).Value.Name;
            int touristId = tourProblemDto.TouristId;
            string content = $"You have a new comment on your report of a tour {tourName}!";
            _notificationService.Create(new NotificationDto(content, NotificationType.TourProblemComment, tourProblemDto.Id, touristId, false));
        }

        [HttpGet("byId")]
        public ActionResult<PagedResult<TourProblemDto>> GetById([FromQuery] int id)
        {
            var result = _tourProblemService.GetById(id);
            return CreateResponse(result);
        }


        [HttpGet("getAll")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll()
        {
            var results = _tourProblemService.GetAll();
            return CreateResponse(results);
        }
    }
}
