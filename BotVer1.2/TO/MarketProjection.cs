﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BotVer1._2.TO
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketProjection
    {
        COMPETITION, EVENT, EVENT_TYPE, MARKET_DESCRIPTION, RUNNER_DESCRIPTION, RUNNER_METADATA
    }
}
