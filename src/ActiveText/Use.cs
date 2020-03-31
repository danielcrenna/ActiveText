// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using ActiveRoutes;
using ActiveText.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveText
{
	public static class Use
	{
		public static IApplicationBuilder UseJsonTextProcessing(this IApplicationBuilder app)
		{
			app.UseJsonMultiCase();
			app.UseJsonTrim();
			app.UseJsonPrettyPrint();
			return app;
		}

		private static void UseJsonMultiCase(this IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				if (context.FeatureEnabled<JsonConversionOptions>(out var options))
				{
					await ExecuteFeature(context, options, next);
				}
				else
				{
					await next();
				}
			});

			static async Task ExecuteFeature(HttpContext c, JsonConversionOptions o, Func<Task> next)
			{
				var qs = c.Request.Query;
				qs.TryGetValue(o.MultiCaseOperator, out var values);

				foreach (var value in values)
				{
					foreach (var entry in c.RequestServices.GetServices<ITextTransform>())
					{
						if (!entry.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
						{
							continue;
						}

						c.Items[Constants.ContextKeys.JsonMultiCase] = entry;
						goto next;
					}
				}

				next:
				await next();
			}
		}

		private static void UseJsonTrim(this IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				if (context.FeatureEnabled<JsonConversionOptions>(out var options))
				{
					await ExecuteFeature(context, options, next);
				}
				else
				{
					await next();
				}
			});

			static async Task ExecuteFeature(HttpContext c, JsonConversionOptions o, Func<Task> next)
			{
				var qs = c.Request.Query;
				qs.TryGetValue(o.TrimOperator, out var values);
				foreach (var value in values)
				{
					if ((!bool.TryParse(value, out var boolean) || !boolean) &&
					    (!int.TryParse(value, out var number) || number != 1))
						continue;
					c.Items[Constants.ContextKeys.JsonTrim] = true;
					goto next;
				}

				next:
				await next();
			}
		}

		private static void UseJsonPrettyPrint(this IApplicationBuilder app)
		{
			app.Use(async (context, next) =>
			{
				if (context.FeatureEnabled<JsonConversionOptions>(out var options))
				{
					await ExecuteFeature(context, options, next);
				}
				else
				{
					await next();
				}
			});

			static async Task ExecuteFeature(HttpContext c, JsonConversionOptions o, Func<Task> next)
			{
				var qs = c.Request.Query;
				qs.TryGetValue(o.PrettyPrintOperator, out var values);
				foreach (var value in values)
				{
					if ((!bool.TryParse(value, out var boolean) || !boolean) &&
					    (!int.TryParse(value, out var number) || number != 1))
						continue;
					c.Items[Constants.ContextKeys.JsonPrettyPrint] = true;
					goto next;
				}

				next:
				await next();
			}
		}
	}
}