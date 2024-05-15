﻿using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enum;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	[ViewModel<Category, CreateCategoryCommand>]
	public class CreateCategoryCommand : BaseAuthorizeRequest<Category>, IWithSrc
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Src { get; set; }
	}
}
