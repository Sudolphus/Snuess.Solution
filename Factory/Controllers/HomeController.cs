using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;
    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Machine> machines = _db.Machines.ToList();
      machines.Sort();
      List<Engineer> engineers = _db.Engineers.ToList();
      engineers.Sort();
      ViewBag.MachineList = machines;
      ViewBag.EngineerList = engineers;
      return View();
    }
  }
}