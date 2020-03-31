// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Buffers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ActiveText
{
	public class JsonOutputFormatter : NewtonsoftJsonOutputFormatter
	{
		private readonly IDictionary<ITextTransform, JsonContractResolver> _resolvers;
		private JsonSerializer _serializer;

		public JsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool,
			IOptions<MvcOptions> options) : base(serializerSettings, charPool, options.Value) =>
			_resolvers = new Dictionary<ITextTransform, JsonContractResolver>();

		public override bool CanWriteResult(OutputFormatterCanWriteContext context)
		{
			EnsureSerializer(context);

			return base.CanWriteResult(context);
		}

		private void EnsureSerializer(OutputFormatterCanWriteContext context)
		{
			if (context.HttpContext.Items[Constants.ContextKeys.JsonMultiCase] is ITextTransform transform)
			{
				if (!_resolvers.TryGetValue(transform, out var resolver))
				{
					resolver = new JsonContractResolver(transform, JsonProcessingDirection.Output);
					_resolvers.Add(transform, resolver);
				}

				_serializer.ContractResolver = resolver;
			}

			if (context.HttpContext.Items[Constants.ContextKeys.JsonPrettyPrint] is bool prettyPrint && !prettyPrint)
			{
				_serializer.Apply(s => s.Formatting = Formatting.None);
			}

			if (context.HttpContext.Items[Constants.ContextKeys.JsonTrim] is bool trim && trim)
			{
				_serializer.Apply(s =>
				{
					s.NullValueHandling = NullValueHandling.Ignore;
					s.DefaultValueHandling = DefaultValueHandling.Ignore;
				});
			}
		}

		protected override JsonSerializer CreateJsonSerializer()
		{
			return _serializer ??= JsonSerializer.Create(SerializerSettings);
		}
	}
}