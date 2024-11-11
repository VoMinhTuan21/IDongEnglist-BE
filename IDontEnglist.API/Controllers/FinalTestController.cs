﻿using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.Features.FinalTests.Commands;
using IDonEnglist.Application.Features.FinalTests.Queries;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.ViewModels.FinalTest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/final-test")]
    [ApiController]
    [Authorize]
    public class FinalTestController : BaseController
    {
        private readonly IMediator _mediator;

        public FinalTestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<FinalTestViewModel>> Create([FromBody] CreateFinalTestDTO createData)
        {
            var command = new CreateFinalTest
            {
                CreateData = createData,
                CurrentUser = GetUserFromToken()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<FinalTestViewModel>>> GetPagination([FromQuery] GetPaginationFinalTestsDTO filter)
        {
            var query = new GetPaginationFinalTests
            {
                FilterData = filter
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}