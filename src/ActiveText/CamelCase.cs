// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using TypeKitchen;

namespace ActiveText
{
	internal class CamelCase : ITextTransform
	{
		public string Name => "Camel";

		public string Transform(string input)
		{
			return Pooling.StringBuilderPool.Scoped(sb =>
			{
				if (input == null)
				{
					return;
				}

				sb.Append(input);

				if (string.IsNullOrWhiteSpace(input) || char.IsLower(input[0]))
				{
					return;
				}

				sb[0] = char.ToLowerInvariant(sb[0]);
			});
		}
	}
}