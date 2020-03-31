// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Buffers;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ActiveText.Configuration
{
	internal class ConfigureTextOptions : IConfigureOptions<MvcOptions>
	{
		private readonly ILoggerFactory _loggerFactory;

		private readonly ArrayPool<char> _charPool;
		private readonly ObjectPoolProvider _objectPoolProvider;
		private readonly JsonSerializerSettings _settings;

		public ConfigureTextOptions(ILoggerFactory loggerFactory, ArrayPool<char> charPool, ObjectPoolProvider objectPoolProvider, JsonSerializerSettings settings)
		{
			_loggerFactory = loggerFactory;
			_charPool = charPool;
			_objectPoolProvider = objectPoolProvider;
			_settings = settings;
		}

		public void Configure(MvcOptions options)
		{
			var logger = _loggerFactory.CreateLogger(Constants.Loggers.Formatters);

			var jsonOptions = new MvcNewtonsoftJsonOptions();
			jsonOptions.Apply(_settings);

			options.InputFormatters.Clear();
			options.OutputFormatters.Clear();
			
			//
			// XML:
			if (string.IsNullOrEmpty(options.FormatterMappings.GetMediaTypeMappingForFormat("xml")))
				options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeNames.Application.Xml);
			options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter(new XmlWriterSettings {Indent = true, NamespaceHandling = NamespaceHandling.OmitDuplicates}, _loggerFactory));
			options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(options));
			
			//
			// JSON:
			if (string.IsNullOrEmpty(options.FormatterMappings.GetMediaTypeMappingForFormat("json")))
				options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeNames.Application.Json);
			options.InputFormatters.Add(new JsonInputFormatter(logger, _settings, _charPool, _objectPoolProvider, options, jsonOptions));
			options.InputFormatters.Add(new JsonPatchInputFormatter(logger, _settings, _charPool, _objectPoolProvider, options, jsonOptions));
			options.OutputFormatters.Add(new JsonOutputFormatter(_settings, _charPool, new OptionsWrapper<MvcOptions>(options)));
		}
	}
}