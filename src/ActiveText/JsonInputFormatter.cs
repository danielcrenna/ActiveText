// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Buffers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;

namespace ActiveText
{
	public sealed class JsonInputFormatter : NewtonsoftJsonInputFormatter
	{
		private readonly IDictionary<ITextTransform, JsonContractResolver> _resolvers;
		private JsonSerializer _serializer;

		public JsonInputFormatter(
			ILogger logger,
			JsonSerializerSettings serializerSettings,
			ArrayPool<char> charPool,
			ObjectPoolProvider objectPoolProvider,
			MvcOptions options,
			MvcNewtonsoftJsonOptions jsonOptions
		) : base(logger, serializerSettings, charPool, objectPoolProvider, options, jsonOptions) =>
			_resolvers = new Dictionary<ITextTransform, JsonContractResolver>();

		public override bool CanRead(InputFormatterContext context)
		{
			EnsureSerializer(context);

			return base.CanRead(context);
		}

		private void EnsureSerializer(InputFormatterContext context)
		{
			if (context.HttpContext.Items[Constants.ContextKeys.JsonMultiCase] is ITextTransform transform)
			{
				if (!_resolvers.TryGetValue(transform, out var resolver))
				{
					resolver = new JsonContractResolver(transform, JsonProcessingDirection.Input);
					_resolvers.Add(transform, resolver);
				}

				_serializer.ContractResolver = resolver;
			}
		}

		protected override JsonSerializer CreateJsonSerializer()
		{
			return _serializer ??= JsonSerializer.Create(SerializerSettings);
		}

		protected override void ReleaseJsonSerializer(JsonSerializer serializer)
		{
			_serializer = null;
		}
	}
}