
using System;
using System.Linq;
using System.Text.Json;

using System.Text.RegularExpressions;

namespace iMaxSys.Max.Json.NamingPolicy
{
    /// <summary>
    /// 下划线命名策略
    /// </summary>
    public class UnderscoreNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            string n = "";
            try
            {
                n = Regex.Replace(name, @"((?<=.)[A-Z][a-z]*)", @"_$1").ToLower();
            }
            catch
            {
            }
            return n;
        }
    }

    /// <summary>
    /// 蛇形/下划线命名策略
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
        }
    }
}
