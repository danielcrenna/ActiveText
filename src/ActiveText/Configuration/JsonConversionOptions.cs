// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ActiveRoutes;
using ActiveRoutes.Meta;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveText.Configuration
{
	public class JsonConversionOptions : IFeatureToggle, IMetaParameterProvider
	{
		public string MultiCaseOperator { get; set; } = Constants.QueryStrings.MultiCase;
		public string EnvelopeOperator { get; set; } = Constants.QueryStrings.Envelope;
		public string TrimOperator { get; set; } = Constants.QueryStrings.Trim;
		public string PrettyPrintOperator { get; set; } = Constants.QueryStrings.PrettyPrint;
		public bool Enabled { get; set; } = true;

		public void Enrich(string url, MetaOperation operation, IServiceProvider serviceProvider)
		{
			if (!Enabled)
				return;

			if (operation.url == null)
				operation.url = MetaUrl.FromRaw(url);

			var transforms = serviceProvider.GetServices<ITextTransform>();
			var cases = transforms.Select(x => x.Name.ToLowerInvariant()).ToList();

			var multiCaseParameter = new MetaParameter
			{
				key = MultiCaseOperator,
				value = cases.FirstOrDefault() ?? string.Empty,
				description =
					$"Transforms responses to alternative cases. Valid values are: {string.Join(", ", cases)}.",
				disabled = true
			};

			var envelopeParameter = new MetaParameter
			{
				key = EnvelopeOperator,
				value = "1",
				description =
					"Transforms responses to include more information in the payload for constrained clients.",
				disabled = true
			};

			var prettyPrintParameter = new MetaParameter
			{
				key = PrettyPrintOperator,
				value = "1",
				description = "Enhances readability of responses by adding whitespace and nesting.",
				disabled = true
			};

			var trimParameter = new MetaParameter
			{
				key = TrimOperator,
				value = "1",
				description = "Reduces response weight by omitting null and default values.",
				disabled = true
			};

			operation.url.query ??= operation.url.query = new List<MetaParameter>();
			operation.url.query.AddRange(new[]
			{
				multiCaseParameter, envelopeParameter, trimParameter, prettyPrintParameter
			});
		}
	}
}