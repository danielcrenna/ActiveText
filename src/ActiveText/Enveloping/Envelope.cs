// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ActiveErrors;
using Microsoft.AspNetCore.Http;

namespace ActiveText.Enveloping
{
	[DataContract]
	[KnownType(nameof(GetKnownTypes))]
	public class Envelope
	{
		[DataMember] public IList<Error> Errors;
		[DataMember] public bool HasErrors;
		[DataMember] public IHeaderDictionary Headers;
		[DataMember] public PagingInfo Paging;
		[DataMember] public int Status;
		private static IEnumerable<Type> GetKnownTypes() { return KnownTypesContext.GetKnownTypes(); }
	}
}