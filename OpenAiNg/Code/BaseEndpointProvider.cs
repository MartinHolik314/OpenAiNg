﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAiNg.Chat;
using OpenAiNg.Vendor.Anthropic;

namespace OpenAiNg.Code;

public abstract class BaseEndpointProvider : IEndpointProvider
{
    public OpenAiApi Api { get; set; }
    public LLmProviders Provider { get; set; } = LLmProviders.Unknown;
    
    internal static readonly JsonSerializerSettings NullSettings = new() { NullValueHandling = NullValueHandling.Ignore };
    
    public BaseEndpointProvider(OpenAiApi api)
    {
        Api = api;
    }

    public abstract string ApiUrl(CapabilityEndpoints endpoint, string? url);
    public abstract T? InboundMessage<T>(string jsonData);
    public abstract IAsyncEnumerable<T?> InboundStream<T>(StreamReader streamReader) where T : ApiResultBase;
    public abstract HttpRequestMessage OutboundMessage(string url, HttpMethod verb, object? data, bool streaming);
    public abstract HashSet<string> ToolFinishReasons { get;  }
}