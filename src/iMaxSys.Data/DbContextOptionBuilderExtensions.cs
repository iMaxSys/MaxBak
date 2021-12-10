using System;
using Microsoft.EntityFrameworkCore;

using iMaxSys.Max.Options;

namespace iMaxSys.Data
{
    public static class OptionBuilderExtensions
    {
        public static DbContextOptionsBuilder OptionBuilderExtensions(this DbContextOptionsBuilder builder, MaxOption maxOption)
        {
            builder.UseMySql"TreatTinyAsBoolean=True", ServerVersion.AutoDetect(""));

            switch (maxOption.Core.Type)
            {
                case 0:

                default:
                    return builder.UseMySql(maxOption.Core.Connection, ServerVersion.AutoDetect);

                    break;
            }
        }
    }
}

