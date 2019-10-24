using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BotVer1._2.TO
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderStatus
    {
        EXECUTION_COMPLETE,
        EXECUTABLE
    }
}
