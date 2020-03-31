// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ActiveStorage;
using Microsoft.AspNetCore.Http;

namespace ActiveText
{
	public interface IPagingInfoProvider
	{
		PagingInfo GetPagingInfo<T>(HttpRequest request, IPage<T> data);
		void SetPagingInfoHeaders<T>(HttpResponse response, IPage<T> data);
	}
}