﻿using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class BaseListQueryHandler<TQuery, TEntity, TResult> : BaseAuthorizeHandler<TQuery, List<TResult>>
		where TQuery : BaseAuthorizeListRequest<List<TResult>>
		where TEntity : WithId, IWithUserId
	{
		private readonly ApplicationContext _context;
		private readonly IMapper _mapper;

		public BaseListQueryHandler(ApplicationContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public override async Task<List<TResult>> Handle(TQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Set<TEntity>()
				.AsNoTracking()
				.Where(q => q.UserId == request.UserId);

			query = await Filters(query, request, cancellationToken);

			var result = await _mapper.ProjectTo<TResult>(query
					.Skip(request.Page - 1)
					.Take(request.Count))
				.ToListAsync();

			return result;
		}

		public virtual Task<IQueryable<TEntity>> Filters(IQueryable<TEntity> query, TQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(query);
		}
	}
}