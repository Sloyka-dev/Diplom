using DataLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace TourApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : Controller
{

    [HttpGet("regions")]
    public ActionResult<Dictionary<int, string>> GetRegions()
    {

        return DataManager.Regions;

    }

    [HttpGet]
    public ActionResult<List<Tour>> GetTour()
    {

        return DataManager.Tours;

    }

    [HttpGet("{id}")]
    public ActionResult<Tour?> GetTour(int id)
    {

        return DataManager.Tours.Where(t => t.Id == id).FirstOrDefault();

    }

}
