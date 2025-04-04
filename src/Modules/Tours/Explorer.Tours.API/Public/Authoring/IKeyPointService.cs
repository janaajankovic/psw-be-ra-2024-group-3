﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Authoring
{
    public interface IKeyPointService
    {
        Result<PagedResult<KeyPointDto>> GetPaged(int page, int pageSize);
        Result<KeyPointDto> Create(KeyPointDto keyPoint);
        Result<KeyPointDto> Get(int id);
        Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId);

        Result<KeyPointDto> GetById(int keyPointId);
        Result<KeyPointDto> GetByStoryId(int id);

        Result<KeyPointDto> PublishKeyPoint(int keyPointId, int flag);
        Result<List<KeyPointDto>> GetPublic();
        Result<KeyPointDto> UpdateList(int id, List<long> ids);

        public Result<KeyPointDto> UpdateKeyPoint(int id, KeyPointDto updatedDto);
        public Result<KeyPointDto> UpdateKeyPointStory(int id, KeyPointDto updatedDto);
        public Result DeleteKeyPoint(int id);
        Result<KeyPointDto> RemoveStoryId(int id);
    }
}
