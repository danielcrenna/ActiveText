// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Runtime.Serialization;

namespace ActiveText
{
	[DataContract]
	public struct PagingInfo
	{
		[DataMember] public long TotalCount;

		[DataMember] public long TotalPages;

		[DataMember] public string FirstPage;

		[DataMember] public string NextPage;

		[DataMember] public string PreviousPage;

		[DataMember] public string LastPage;
	}
}