
using iMaxSys.Max.Caching;

namespace iMaxSys.Max.Services.Increase;

public class DayIncreaseService : ISerialService
{
    const string KEY = "sr:";

    public readonly ICache _cache;

    public DayIncreaseService(IGenericCache _genericCache)
    {
        _cache = _genericCache;
    }

    public async Task<int> Next(string id)
    {

        DaySerail serial = await _cache.GetAsync<DaySerail>($"{KEY}{id}");
        if (serial == null)
        {
            serial = new DaySerail
            {
                Serail = 1,
                Date = DateTime.Now.Date
            };
        }
        else
        {
            if (serial.Date == DateTime.Now.Date)
            {
                serial.Serail++;
            }
            else
            {
                serial = new DaySerail
                {
                    Serail = 1,
                    Date = DateTime.Now.Date
                };
            }
        }

        await _cache.SetAsync($"{KEY}{id}", serial);
        return serial.Serail;
    }
}

/// <summary>
/// 每日序列
/// </summary>
public class DaySerail
{
    public int Serail { get; set; }

    public DateTime Date { get; set; }
}
