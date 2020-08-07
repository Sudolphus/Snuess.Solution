using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public ActionResult Index(string searchString)
    {
      IQueryable<Machine> machineQuery = _db.Machines;
      IQueryable<Engineer> engineerQuery = _db.Engineers;
      if (!string.IsNullOrEmpty(searchString))
      {
        Regex search = new Regex(searchString, RegexOptions.IgnoreCase);
        machineQuery = machineQuery.Where(mach => search.IsMatch(mach.Name));
        engineerQuery = engineerQuery.Where(eng => search.IsMatch(eng.FullName));
      }
      List<Machine> machines = machineQuery.ToList();
      machines.Sort();
      List<Engineer> engineers = engineerQuery.ToList();
      engineers.Sort();
      ViewBag.MachineList = machines;
      ViewBag.EngineerList = engineers;
      return View();
    }

    [HttpPost]
    public ActionResult Index(string searchString, string dummy)
    {
      return RedirectToAction("Index", new { searchString = searchString });
    }
  }
}