﻿using System;

namespace Csp.Logger
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
