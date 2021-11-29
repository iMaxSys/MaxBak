using Microsoft.AspNetCore.Mvc;

namespace Phoenix.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TaxController : ControllerBase
{
    [HttpGet]
    public decimal Calc()
    {
        return 99.82M;
    }
}

public class Order
{
    public string? Id { get; set; }

    public decimal Total { get; set; }

    public IList<Item>? Items { get; set; }
}

public class Item
{
    public string? Id { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }

    public decimal Total { get; set; }
}