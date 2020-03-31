// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ActiveText
{
	public static class HttpContextExtensions
	{
		public static async Task WriteResultAsJson(this IApplicationBuilder app, HttpContext context, string json,
			CancellationToken? cancellationToken = null)
		{
			context.Response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Application.Json);
			await context.Response.WriteAsync(json, cancellationToken ?? context.RequestAborted);
		}

		public static async Task WriteResultAsJson(this IApplicationBuilder app, HttpContext context, object instance,
			CancellationToken? cancellationToken = null)
		{
			context.Response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Application.Json);
			await context.Response.WriteAsync(SerializeObject(app, context, instance),
				cancellationToken ?? context.RequestAborted);
		}

		public static string SerializeObject(this IApplicationBuilder app, HttpContext context, object instance)
		{
			var serializerSettings = app.ApplicationServices.GetService<JsonSerializerSettings>();
			if (serializerSettings != null)
			{
				if (context.Items[Constants.ContextKeys.JsonMultiCase] is ITextTransform transform)
				{
					serializerSettings.ContractResolver =
						new JsonContractResolver(transform, JsonProcessingDirection.Output);
				}
				else
				{
					serializerSettings = JsonConvert.DefaultSettings();
				}
			}

			var json = serializerSettings != null
				? JsonConvert.SerializeObject(instance, serializerSettings)
				: JsonConvert.SerializeObject(instance);

			return json;
		}
	}
}