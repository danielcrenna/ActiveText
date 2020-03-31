using ActiveRoutes.Meta;
using ActiveText.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ActiveText
{
	public static class Add
	{
		public static IServiceCollection AddJsonTextProcessing(this IServiceCollection services)
		{
			services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureTextOptions>();
			services.AddSingleton(r => JsonConvert.DefaultSettings());
			services.AddScoped<IMetaParameterProvider>(r => r.GetRequiredService<IOptionsSnapshot<JsonConversionOptions>>().Value);
			return services;
		}

		
	}
}
