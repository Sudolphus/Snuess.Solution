using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      IQueryable<Machine> machineQuery = _db.Machines;
      IEnumerable<Machine> machineList = machineQuery
        .ToList()
        .OrderBy(machines => machines.Name);
      return View(machineList);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine newMachine)
    {
      _db.Machines.Add(newMachine);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = newMachine.MachineId});
    }

    public ActionResult Details(int id)
    {
      Machine machine = _db.Machines
        .Include(machines => machines.Engineers)
        .ThenInclude(join => join.Engineer)
        .First(machines => machines.MachineId == id);
      List<Engineer> engineerList = new List<Engineer>();
      foreach(EngineerMachine entry in machine.Engineers)
      {
        engineerList.Add(entry.Engineer);
      }
      engineerList.Sort();
      ViewBag.EngineerList = engineerList;
      return View(machine);
    }

    public ActionResult Edit(int id)
    {
      Machine machine = _db.Machines.First(machines => machines.MachineId == id);
      return View(machine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine machine = _db.Machines.First(machines => machines.MachineId == id);
      return View(machine);
    }

    [HttpPost]
    public ActionResult Delete(Machine machine)
    {
      _db.Machines.Remove(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddEngineer(int id)
    {
      Machine machine = _db.Machines.First(machines => machines.MachineId == id);
      List<Engineer> engineers = _db.Engineers.ToList();
      engineers.Sort();
      ViewBag.EngineerId = new SelectList(engineers, "EngineerId", "FirstName LastName");
      return View(machine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int engineerId)
    {
      EngineerMachine join = null;
      try
      {
        join = _db.EngineerMachine
          .Where(entry => entry.MachineId == machine.MachineId)
          .First(entry => entry.EngineerId == engineerId);
      }
      catch
      {
        _db.EngineerMachine.Add(new EngineerMachine() {MachineId = machine.MachineId, EngineerId = engineerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult RemoveEngineer(int id)
    {
      Machine machine = _db.Machines
        .Include(entry => entry.Engineers)
        .ThenInclude(eng => eng.Engineer)
        .First(machines => machines.MachineId == id);
      List<Engineer> engineers = new List();
      foreach(EngineerMachine entry in machine.Engineers)
      {
        engineers.Add(entry.Engineer);
      }
      engineers.Sort();
      ViewBag.EnginerId = new SelectList(engineers, "EngineerId", "FirstName LastName");
      return View(machine);
    }

    [HttpPost]
    public ActionResult RemoveEngineer(Machine machine, int engineerId)
    {
      EngineerMachine join = _db.EngineerMachine
        .Where(entry => entry.MachineId == machine.MachineId)
        .First(entry => entry.EngineerId == engineerId);
      _db.EngineerMachine.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }
  }
}