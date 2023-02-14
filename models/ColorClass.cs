using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proj.models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ColorClass
    {
        Black = 0,
        White = 1,
        Brown = 2
    }
}