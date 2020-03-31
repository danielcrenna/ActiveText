// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace ActiveText
{
	public static class Constants
	{
		public static class ContextKeys
		{
			public const string JsonMultiCase = nameof(JsonMultiCase);
			public const string JsonTrim = nameof(JsonTrim);
			public const string JsonPrettyPrint = nameof(JsonPrettyPrint);
		}

		public static class QueryStrings
		{
			public const string MultiCase = "case";
			public const string Envelope = "envelope";
			public const string Trim = "trim";
			public const string PrettyPrint = "prettyPrint";
		}

		public static class Loggers
		{
			public const string Formatters = nameof(Formatters);
		}
	}
}