﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Authoring
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Create(TourDto tour);
        Result<List<TourDto>> GetByAuthorId(int page, int pageSize, int id);
        Result<bool> Publish(TourDto entity);
        Result<bool> Archive(TourDto entity);
        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId);
        public Result<List<TourDto>> GetAllToursWithKeyPoints();
    }
}
