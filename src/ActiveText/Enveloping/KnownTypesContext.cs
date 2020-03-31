// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ActiveErrors;

namespace ActiveText.Enveloping
{
	public static class KnownTypesContext
	{
		public static Type[] KnownTypes { get; set; }

		public static IEnumerable<Type> GetKnownTypes()
		{
			yield return typeof(Error);
			yield return typeof(PagingInfo);
			foreach (var type in KnownTypes)
			{
				yield return type;
			}
		}
	}
}