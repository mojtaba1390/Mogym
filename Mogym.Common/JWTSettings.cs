﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Common
{
    public static class JWTSettings
    {
        public const string SectionName = "JwtSettings";
        public static string Secret { get; } = "ThisIsTheMogymSecret";
        public static int ExpireTime { get; } = 1;
        public static string Issuer { get; } = "http://localhost:61955";
        public static string Audiance { get; } = "http://localhost:4200";
    }
}
