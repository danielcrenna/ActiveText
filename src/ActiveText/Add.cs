// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ActiveRoutes.Meta;
using ActiveText.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ActiveText
{
	public static class Add
	{
		public static IServiceCollection AddJsonTextProcessing(this IServiceCollection services)
		{
			services.TryAddSingleton<IEnumerable<ITextTransform>>(r => new ITextTransform[] {new CamelCase(), new SnakeCase(), new PascalCase()});
			services.TryAddSingleton<IConfigureOptions<MvcOptions>, ConfigureTextOptions>();
			services.TryAddSingleton(r => JsonConvert.DefaultSettings());
			services.TryAddEnumerable(ServiceDescriptor.Scoped<IMetaParameterProvider>(r => r.GetRequiredService<IOptionsSnapshot<JsonConversionOptions>>().Value));
			return services;
		}
	}
}