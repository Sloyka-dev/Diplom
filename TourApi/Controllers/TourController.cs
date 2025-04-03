using DataLib.Models;
using DataLib.Utility;
using Microsoft.AspNetCore.Mvc;

namespace TourApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : Controller
{

    [HttpGet("regions")]
    public ActionResult<Dictionary<int, string>> GetRegions()
    {

        return Tour.Regions;

    }

    [HttpGet]
    public ActionResult<List<Tour>> Search([FromQuery] string search = "")
    {

        var data = DataManager.Tours.Where(t => t.ForPeopleWithDisabilities == true);
        var res = FuzySearch.SearchTours(data.ToList(), search);

        Console.WriteLine(res.Count);

        return res;

    }

}
