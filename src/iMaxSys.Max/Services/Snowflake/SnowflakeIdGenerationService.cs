//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SnowflakeIdGenerationService.cs
//摘要: SnowflakeIdGenerationService
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Services.Snowflake;

/// <summary>
/// SnowflakeIdGenerationService
/// </summary>
public class SnowflakeIdGenerationService : IIdGenerationService
{
    private readonly SnowflakeWorker _worker;

    public SnowflakeIdGenerationService()
    {
        _worker = new SnowflakeWorker(0, 0);
    }

    public long GenerateId()
    {
        return _worker.NextId();
    }
}
