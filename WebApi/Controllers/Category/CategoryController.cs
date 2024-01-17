﻿using AutoMapper;
using Domain.Models;
using Infrastructure.Commands;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class CategoryController : BaseApplicationController
	{
		private readonly IMapper _mapper;

		public CategoryController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategory(string id, CancellationToken cancellationToken)
		{
			var query = new GetCategoryQuery
			{
				Id = id,
				UserId = UserId
			};

			var categoryViewModel = await _mediator.Send(query, cancellationToken);

			return Ok(categoryViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var category = await _mediator.Send(command, cancellationToken);

			var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

			return Ok(categoryViewModel);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var category = await _mediator.Send(command, cancellationToken);

			var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

			return Ok(categoryViewModel);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var deleted = await _mediator.Send(command, cancellationToken);

			return Ok(deleted);
		}
	}
}