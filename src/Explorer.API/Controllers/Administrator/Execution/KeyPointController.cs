using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Authoring;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Execution
{

    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/keyPoint")]
    public class KeyPointController : BaseApiController
    {

        private readonly IKeyPointService _keyPointService;
        private readonly IPublishRequestService _publishRequestService;
        private readonly INotificationService _notificationService;

        public KeyPointController(IKeyPointService keyPointService, IPublishRequestService publishRequestService, INotificationService notificationService)
        {
            _keyPointService = keyPointService;
            _publishRequestService = publishRequestService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public ActionResult<PagedResult<KeyPointDto>> GetById([FromQuery] int id)
        {
            var results = _keyPointService.GetById(id);
            return CreateResponse(results);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PublishRequestDto> ChangeKeyPointStatus([FromBody] PublishRequestDto publishRequest)
        {
            // Update the publish request
            var result = _publishRequestService.Update(publishRequest);

            if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Accepted)
            {
                _keyPointService.PublishKeyPoint(publishRequest.EntityId);
                
            }
            //ovdje dodaj za decline
            if (publishRequest.Status == PublishRequestDto.RegistrationRequestStatus.Rejected)
            {
                _keyPointService.PublishKeyPoint(publishRequest.EntityId, 1);
                notifyAccepted(result.Value);
            }

            return CreateResponse(result);
        }


    

    private void notifyRejected(PublishRequestDto req)
    {
        int tourAuthorId = req.AuthorId;
        string content = $"Your public keypoint request has been rejected";
        _notificationService.Create(new NotificationDto(content, NotificationType.PublicRequest, req.Id, tourAuthorId, false));
    }
}
}
