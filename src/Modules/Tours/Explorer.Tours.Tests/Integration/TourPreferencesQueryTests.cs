﻿using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourPreferencesQueryTests : BaseToursIntegrationTest
    {
        public TourPreferencesQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourPreferencesDto>;

            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        private static TourPreferencesController CreateController(IServiceScope scope)
        {
            return new TourPreferencesController(scope.ServiceProvider.GetRequiredService<ITourPreferencesService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
