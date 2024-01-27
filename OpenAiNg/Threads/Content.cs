// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using OpenAiNg.Common;
namespace OpenAiNg.Threads;

public sealed class Content
{
    [JsonProperty("type")]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter<ContentType>))]
    public ContentType Type { get; private set; }
    
    [JsonProperty("text")]
    public TextContent Text { get; private set; }
    
    [JsonProperty("image_file")]
    public ImageFile ImageFile { get; private set; }

    public override string ToString() => Type switch
    {
        ContentType.Text => Text.Value,
        ContentType.ImageFile => ImageFile.FileId,
        _ => throw new ArgumentOutOfRangeException()
    };
}