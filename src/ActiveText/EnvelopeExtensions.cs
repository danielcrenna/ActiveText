// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Net;
using ActiveErrors;
using Microsoft.AspNetCore.Http;
using ActiveStorage;
using ActiveText.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveText
{
	public static class EnrichmentExtensions
	{
		public static void MaybeEnvelope<T>(this HttpResponse response, HttpRequest request, JsonConversionOptions options, IPage<T> data, IList<Error> errors, out object body)
		{
			var provider = request.HttpContext.RequestServices.GetService<IPagingInfoProvider>();

			if (FeatureRequested(request, options.EnvelopeOperator))
			{
				var envelope = new EnvelopeCollectionBody<T>
				{
					Data = data,
					Status = response.StatusCode,
					Headers = response.Headers,
					Errors = errors,
					HasErrors = errors?.Count > 0
				};

				body = envelope;
				
				if (provider != null)
					envelope.Paging = provider.GetPagingInfo(request, data);

				response.StatusCode = (int) HttpStatusCode.OK;
				return;
			}

			provider?.SetPagingInfoHeaders(response, data);

			body = new NestedCollectionBody<T> {Data = data, Errors = errors, HasErrors = errors?.Count > 0};
		}

		public static void MaybeEnvelope<T>(this HttpResponse response, HttpRequest request, JsonConversionOptions options, IStream<T> data, IList<Error> errors, out object body)
		{
			if (FeatureRequested(request, options.EnvelopeOperator))
			{
				body = new EnvelopeCollectionBody<T>
				{
					Data = data,
					Status = response.StatusCode,
					Headers = response.Headers,
					Errors = errors,
					HasErrors = errors?.Count > 0
				};
			}
			else
			{
				body = new NestedCollectionBody<T> {Data = data, Errors = errors, HasErrors = errors?.Count > 0};
			}

			response.StatusCode = (int) HttpStatusCode.OK;
		}

		public static void MaybeEnvelope<T>(this HttpResponse response, HttpRequest request, JsonConversionOptions options,
			T data, IList<Error> errors, out object body)
		{
			if (FeatureRequested(request, options.EnvelopeOperator))
			{
				body = new EnvelopeBody<T>
				{
					Data = data,
					Status = response.StatusCode,
					Headers = response.Headers,
					Errors = errors,
					HasErrors = errors?.Count > 0
				};
			}
			else
			{
				body = new NestedBody<T> {Data = data, Errors = errors, HasErrors = errors?.Count > 0};
			}

			response.StatusCode = (int) HttpStatusCode.OK;
		}

		public static void MaybeEnvelope(this HttpResponse response, HttpRequest request, JsonConversionOptions options, IList<Error> errors, out object body)
		{
			if (FeatureRequested(request, options.EnvelopeOperator))
			{
				body = new Envelope
				{
					Status = response.StatusCode,
					Headers = response.Headers,
					Errors = errors,
					HasErrors = errors?.Count > 0
				};
			}
			else
			{
				body = new Nested {Errors = errors, HasErrors = errors?.Count > 0};
			}

			response.StatusCode = (int) HttpStatusCode.OK;
		}

		public static bool FeatureRequested(this HttpRequest request, string @operator)
		{
			if (string.IsNullOrWhiteSpace(@operator))
			{
				return false;
			}

			bool useFeature;
			if (request.Query.TryGetValue(@operator, out var values))
			{
				bool.TryParse(values, out useFeature);
			}
			else
			{
				useFeature = false;
			}

			return useFeature;
		}
	}
}