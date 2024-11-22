﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class KeyPointService : CrudService<KeyPointDto, KeyPoint>, IKeyPointService
    {

        private readonly ICrudRepository<KeyPoint> _repository;
        private readonly IMapper _mapper;
        private readonly IKeyPointRepository _keyPointRepository;

        public KeyPointService(ICrudRepository<KeyPoint> repository, IMapper mapper, IKeyPointRepository keyRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _keyPointRepository = keyRepository;
        }


        public Result<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)
        {

            var pagedResult = _repository.GetPaged(1, int.MaxValue);


            if (pagedResult == null || pagedResult.Results == null || !pagedResult.Results.Any())
            {
                return Result.Fail<List<KeyPointDto>>("No key points found for this tour.");
            }


            var keyPoints = pagedResult.Results.Where(kp => kp.TourId == tourId).ToList();


            var keyPointDtos = _mapper.Map<List<KeyPointDto>>(keyPoints);
            return Result.Ok(keyPointDtos);
        }

        public Result<KeyPointDto> GetById(int id)
        {
            var keyPoint = _keyPointRepository.GetByIdAsync(id);
            if (keyPoint == null)
                return Result.Fail("Publish request not found");

            return Result.Ok(MapToDto(keyPoint));
        }

        public Result<KeyPointDto> Update(int id)
        {
            var keyPointDto = GetById(id);
            if (keyPointDto == null)
                return Result.Fail("Publish request not found");

            var keyPoint = MapToDomain(keyPointDto.Value);
            if (keyPoint == null)
            {
                return Result.Fail("Key point not found");
            }

            keyPoint.UpdateKeyPointStatus(KeyPointStatus.Published);
            var updatedKeyPointDto = _mapper.Map<KeyPointDto>(keyPoint);
            Update(updatedKeyPointDto);
         
            return Result.Ok(updatedKeyPointDto);

        }

    }
}
