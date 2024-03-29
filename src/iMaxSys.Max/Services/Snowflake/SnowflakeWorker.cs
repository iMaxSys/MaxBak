﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: SnowflakeWorker.cs
//摘要: SnowflakeWorker
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Services.Snowflake;

/// <summary>
/// SnowflakeWorker
/// </summary>
public class SnowflakeWorker
{
    public static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public const long Twepoch = 1268096000000L;

    const int WorkerIdBits = 5;
    const int DatacenterIdBits = 5;
    const int SequenceBits = 12;
    const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
    const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

    private const int WorkerIdShift = SequenceBits;
    private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
    public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
    private const long SequenceMask = -1L ^ (-1L << SequenceBits);

    private long _sequence = 0L;
    private long _lastTimestamp = -1L;


    public SnowflakeWorker(long workerId, long datacenterId, long sequence = 0L)
    {
        WorkerId = workerId;
        DatacenterId = datacenterId;
        _sequence = sequence;

        // sanity check for workerId
        if (workerId > MaxWorkerId || workerId < 0)
        {
            throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0", MaxWorkerId));
        }

        if (datacenterId > MaxDatacenterId || datacenterId < 0)
        {
            throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
        }

        //log.info(
        //    String.Format("worker starting. timestamp left shift {0}, datacenter id bits {1}, worker id bits {2}, sequence bits {3}, workerid {4}",
        //                  TimestampLeftShift, DatacenterIdBits, WorkerIdBits, SequenceBits, workerId)
        //    );	
    }

    public long WorkerId { get; protected set; }
    public long DatacenterId { get; protected set; }

    public long Sequence
    {
        get { return _sequence; }
        internal set { _sequence = value; }
    }

    // def get_timestamp() = System.currentTimeMillis

    readonly object _lock = new Object();

    public virtual long NextId()
    {
        lock (_lock)
        {
            var timestamp = InternalCurrentTime();

            if (timestamp < _lastTimestamp)
            {
                //exceptionCounter.incr(1);
                //log.Error("clock is moving backwards.  Rejecting requests until %d.", _lastTimestamp);
                throw new InvalidOperationException(String.Format(
                    "Clock moved backwards.  Refusing to generate id for {0} milliseconds", _lastTimestamp - timestamp));
            }

            if (_lastTimestamp == timestamp)
            {
                _sequence = (_sequence + 1) & SequenceMask;
                if (_sequence == 0)
                {
                    timestamp = TilNextMillis(_lastTimestamp);
                }
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = timestamp;
            var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                     (DatacenterId << DatacenterIdShift) |
                     (WorkerId << WorkerIdShift) | _sequence;

            return id;
        }
    }

    protected virtual long TilNextMillis(long lastTimestamp)
    {
        var timestamp = InternalCurrentTime();
        while (timestamp <= lastTimestamp)
        {
            timestamp = InternalCurrentTime();
        }
        return timestamp;
    }

    private static long InternalCurrentTime()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }
}