// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using TypeKitchen;

namespace ActiveText
{
	internal class SnakeCase : ITextTransform
	{
		public string Name => "Snake";

		public string Transform(string input)
		{
			if (string.IsNullOrEmpty(input) || input.Length > 0 && char.IsLower(input[0]))
			{
				return input;
			}

			return Pooling.StringBuilderPool.Scoped(sb =>
			{
				sb.Append(char.ToLowerInvariant(input[0]));
				for (var i = 1; i < input.Length; i++)
				{
					if (char.IsLower(input[i]))
					{
						sb.Append(input[i]);
					}
					else
					{
						sb.Append('_');
						sb.Append(char.ToLowerInvariant(input[i]));
					}
				}
			});
		}
	}
}