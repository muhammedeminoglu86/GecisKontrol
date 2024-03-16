using GecisKontrol.Domain.Interfaces;
using GecisKontrol.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GecisKontrol.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{ var errorLogService = context.RequestServices.GetRequiredService<IErrorLogService>();

				var errorLog = new ErrorLog
				{
					ErrorName = ex.Message,
					CreationDate = DateTime.UtcNow,
				};

				await errorLogService.AddErrorLogAsync(errorLog);
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var result = new
			{
				StatusCode = context.Response.StatusCode,
				Message = "Internal Server Error. An unexpected error occurred.",
				DetailedMessage = exception.Message
			};
			return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
		}
	}
}
