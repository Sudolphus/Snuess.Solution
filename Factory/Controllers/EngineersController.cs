using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      IQueryable<Engineer> engineerQuery = _db.Engineers;
      IEnumerable<Engineer> engineerList = engineerQuery
        .ToList()
        .OrderBy(eng => eng.LastName)
        .ThenBy(eng => eng.FirstName);
      return View(engineerList);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer newEngineer)
    {
      _db.Engineers.Add(newEngineer);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = newEngineer.EngineerId });
    }

    public ActionResult Details(int id)
    {
      Engineer engineer = _db.Engineers
        .Include(eng => eng.Machines)
        .ThenInclude(join => join.Machine)
        .First(eng => eng.EngineerId == id);
      List<Machine> licenses = new List<Machine>();
      foreach(EngineerMachine entry in engineer.Machines)
      {
        licenses.Add(entry.Machine);
      }
      licenses.Sort();
      ViewBag.Licenses = licenses;
      return View(engineer);
    }

    public ActionResult Edit(int id)
    {
      Engineer engineer = _db.Engineers.First(eng => eng.EngineerId == id);
      return View(engineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer engineer = _db.Engineers.First(eng => eng.EngineerId == id);
      return View(engineer);
    }

    [HttpPost]
    public ActionResult Delete(Engineer engineer)
    {
      _db.Engineers.Remove(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddLicense(int id)
    {
      Engineer engineer = _db.Engineers.First(eng => eng.EngineerId == id);
      List<Machine> machines = _db.Machines.ToList();
      machines.Sort();
      ViewBag.MachineId = new SelectList(machines, "MachineId", "Name");
      return View(engineer);
    }

    [HttpPost]
    public ActionResult AddLicense(Engineer engineer, int machineId)
    {
      _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId });
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult RemoveLicense(int id)
    {
      Engineer engineer = _db.Engineers
        .Include(eng => eng.Machines)
        .ThenInclude(join => join.Machine)
        .First(eng => eng.EngineerId == id);
      List<Machine> licenses = new List<Machine>();
      foreach(EngineerMachine entry in engineer.Machines)
      {
        licenses.Add(entry.Machine);
      }
      licenses.Sort();
      ViewBag.MachineId = new SelectList(licenses, "MachineId", "Name");
      return View(engineer);
    }

    [HttpPost]
    public ActionResult RemoveLicense(Engineer engineer, int machineId)
    {
      EngineerMachine join = _db.EngineerMachine
        .Where(entry => entry.EngineerId == engineer.EngineerId)
        .First(entry => entry.MachineId == machineId);
      _db.EngineerMachine.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }
  }
}