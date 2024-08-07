﻿using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class ImageController : BaseApplicationController
	{
		private readonly IImageService _imageService;

		public ImageController(IMediator mediator, IImageService imageService) : base(mediator)
		{
			_imageService = imageService;
		}

		/// <summary>
		/// Upload an image to the file system
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with Src image</returns>
		/// <exception cref="LogicException">Invalid image.</exception>
		[HttpPost]
		public async Task<IActionResult> Upload(CancellationToken cancellationToken)
		{
			var files = HttpContext.Request.Form.Files;

			if (!files.Any())
			{
				throw new LogicException("Invalid image.");
			}

			var image = files[0];

			var src = await _imageService.Upload(image, UserId, cancellationToken);

			return Ok(new { Src = src });
		}
	}
}
