// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ActiveText
{
	public static class MvcJsonOptionsExtensions
	{
		public static void Apply(this MvcNewtonsoftJsonOptions options, JsonSerializerSettings settings)
		{
			options.SerializerSettings.CheckAdditionalContent = settings.CheckAdditionalContent;
			options.SerializerSettings.ConstructorHandling = settings.ConstructorHandling;
			options.SerializerSettings.Context = settings.Context;
			options.SerializerSettings.ContractResolver = settings.ContractResolver;
			options.SerializerSettings.Converters = settings.Converters;
			options.SerializerSettings.Culture = settings.Culture;
			options.SerializerSettings.DateFormatHandling = settings.DateFormatHandling;
			options.SerializerSettings.DateFormatString = settings.DateFormatString;
			options.SerializerSettings.DateParseHandling = settings.DateParseHandling;
			options.SerializerSettings.DateTimeZoneHandling = settings.DateTimeZoneHandling;
			options.SerializerSettings.DefaultValueHandling = settings.DefaultValueHandling;
			options.SerializerSettings.EqualityComparer = settings.EqualityComparer;
			options.SerializerSettings.Error = settings.Error;
			options.SerializerSettings.FloatFormatHandling = settings.FloatFormatHandling;
			options.SerializerSettings.FloatParseHandling = settings.FloatParseHandling;
			options.SerializerSettings.Formatting = settings.Formatting;
			options.SerializerSettings.MaxDepth = settings.MaxDepth;
			options.SerializerSettings.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			options.SerializerSettings.MissingMemberHandling = settings.MissingMemberHandling;
			options.SerializerSettings.NullValueHandling = settings.NullValueHandling;
			options.SerializerSettings.ObjectCreationHandling = settings.ObjectCreationHandling;
			options.SerializerSettings.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			options.SerializerSettings.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			options.SerializerSettings.ReferenceResolverProvider = settings.ReferenceResolverProvider;
			options.SerializerSettings.SerializationBinder = settings.SerializationBinder;
			options.SerializerSettings.StringEscapeHandling = settings.StringEscapeHandling;
			options.SerializerSettings.TraceWriter = settings.TraceWriter;
			options.SerializerSettings.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			options.SerializerSettings.TypeNameHandling = settings.TypeNameHandling;
		}

		public static void Apply(this JsonSerializerSettings value, JsonSerializerSettings settings)
		{
			value.CheckAdditionalContent = settings.CheckAdditionalContent;
			value.ConstructorHandling = settings.ConstructorHandling;
			value.Context = settings.Context;
			value.ContractResolver = settings.ContractResolver;
			value.Converters = settings.Converters;
			value.Culture = settings.Culture;
			value.DateFormatHandling = settings.DateFormatHandling;
			value.DateFormatString = settings.DateFormatString;
			value.DateParseHandling = settings.DateParseHandling;
			value.DateTimeZoneHandling = settings.DateTimeZoneHandling;
			value.DefaultValueHandling = settings.DefaultValueHandling;
			value.EqualityComparer = settings.EqualityComparer;
			value.Error = settings.Error;
			value.FloatFormatHandling = settings.FloatFormatHandling;
			value.FloatParseHandling = settings.FloatParseHandling;
			value.Formatting = settings.Formatting;
			value.MaxDepth = settings.MaxDepth;
			value.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			value.MissingMemberHandling = settings.MissingMemberHandling;
			value.NullValueHandling = settings.NullValueHandling;
			value.ObjectCreationHandling = settings.ObjectCreationHandling;
			value.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			value.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			value.ReferenceResolverProvider = settings.ReferenceResolverProvider;
			value.SerializationBinder = settings.SerializationBinder;
			value.StringEscapeHandling = settings.StringEscapeHandling;
			value.TraceWriter = settings.TraceWriter;
			value.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			value.TypeNameHandling = settings.TypeNameHandling;
		}

		public static void Apply(this JsonSerializer value, Action<JsonSerializerSettings> configureSettings)
		{
			var settings = new JsonSerializerSettings();
			settings.Apply(value);
			configureSettings?.Invoke(settings);
			settings.Apply(value);
		}

		public static void Apply(this JsonSerializer value, JsonSerializerSettings settings)
		{
			value.CheckAdditionalContent = settings.CheckAdditionalContent;
			value.ConstructorHandling = settings.ConstructorHandling;
			value.Context = settings.Context;
			value.ContractResolver = settings.ContractResolver;
			value.Converters.Clear();
			foreach (var converter in settings.Converters)
			{
				value.Converters.Add(converter);
			}

			value.Culture = settings.Culture;
			value.DateFormatHandling = settings.DateFormatHandling;
			value.DateFormatString = settings.DateFormatString;
			value.DateParseHandling = settings.DateParseHandling;
			value.DateTimeZoneHandling = settings.DateTimeZoneHandling;
			value.DefaultValueHandling = settings.DefaultValueHandling;
			value.EqualityComparer = settings.EqualityComparer;
			value.Error += settings.Error;
			value.FloatFormatHandling = settings.FloatFormatHandling;
			value.FloatParseHandling = settings.FloatParseHandling;
			value.Formatting = settings.Formatting;
			value.MaxDepth = settings.MaxDepth;
			value.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			value.MissingMemberHandling = settings.MissingMemberHandling;
			value.NullValueHandling = settings.NullValueHandling;
			value.ObjectCreationHandling = settings.ObjectCreationHandling;
			value.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			value.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			value.ReferenceResolver = settings.ReferenceResolverProvider?.Invoke();
			value.SerializationBinder = settings.SerializationBinder;
			value.StringEscapeHandling = settings.StringEscapeHandling;
			value.TraceWriter = settings.TraceWriter;
			value.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			value.TypeNameHandling = settings.TypeNameHandling;
		}

		public static void Apply(this JsonSerializerSettings value, JsonSerializer serializer)
		{
			value.CheckAdditionalContent = serializer.CheckAdditionalContent;
			value.ConstructorHandling = serializer.ConstructorHandling;
			value.Context = serializer.Context;
			value.ContractResolver = serializer.ContractResolver;
			value.Converters.Clear();
			foreach (var converter in serializer.Converters)
			{
				value.Converters.Add(converter);
			}

			value.Culture = serializer.Culture;
			value.DateFormatHandling = serializer.DateFormatHandling;
			value.DateFormatString = serializer.DateFormatString;
			value.DateParseHandling = serializer.DateParseHandling;
			value.DateTimeZoneHandling = serializer.DateTimeZoneHandling;
			value.DefaultValueHandling = serializer.DefaultValueHandling;
			value.EqualityComparer = serializer.EqualityComparer;
			value.FloatFormatHandling = serializer.FloatFormatHandling;
			value.FloatParseHandling = serializer.FloatParseHandling;
			value.Formatting = serializer.Formatting;
			value.MaxDepth = serializer.MaxDepth;
			value.MetadataPropertyHandling = serializer.MetadataPropertyHandling;
			value.MissingMemberHandling = serializer.MissingMemberHandling;
			value.NullValueHandling = serializer.NullValueHandling;
			value.ObjectCreationHandling = serializer.ObjectCreationHandling;
			value.PreserveReferencesHandling = serializer.PreserveReferencesHandling;
			value.ReferenceLoopHandling = serializer.ReferenceLoopHandling;
			value.ReferenceResolverProvider = () => serializer.ReferenceResolver;
			value.SerializationBinder = serializer.SerializationBinder;
			value.StringEscapeHandling = serializer.StringEscapeHandling;
			value.TraceWriter = serializer.TraceWriter;
			value.TypeNameAssemblyFormatHandling = serializer.TypeNameAssemblyFormatHandling;
			value.TypeNameHandling = serializer.TypeNameHandling;
		}
	}
}