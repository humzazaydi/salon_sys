﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokyo.Infrastructure.Caching
{
    public class CacheSettings
    {
        public bool UseDistributedCache { get; set; }
        public bool PreferRedis { get; set; }
        public string RedisURL { get; set; }
    }
}
