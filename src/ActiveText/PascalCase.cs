// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveText
{
	internal class PascalCase : ITextTransform
	{
		public string Name => "Pascal";

		public string Transform(string input)
		{
			return input;
		}
	}
}