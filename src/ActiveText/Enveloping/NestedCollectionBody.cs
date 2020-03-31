// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ActiveText.Enveloping
{
	[DataContract]
	[KnownType(nameof(GetKnownTypes))]
	public class NestedCollectionBody<T> : Nested
	{
		[DataMember] public IEnumerable<T> Data;
		private static IEnumerable<Type> GetKnownTypes() { return KnownTypesContext.GetKnownTypes(); }
	}
}