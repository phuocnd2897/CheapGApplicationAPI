﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApi.Entities.Common.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Unix Epoch value.
        /// </summary>
        public static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a DateTime to a Unix timestamp.
        /// </summary>
        public static int DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            return (int)(dateTime - epoch).TotalSeconds;
        }
    }
}
