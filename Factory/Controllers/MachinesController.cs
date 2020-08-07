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
      return View("Details", new { id = newMachine.MachineId});
    }

    public ActionResult Details(int id)
    {
      Machine machine = _db.Machines
        .First(machines => machines.MachineId == id);
      IEnumerable<Engineer> engineerList = (IQueryable<Engineer>) _db.EngineerMachine
        .Where(entry => entry.MachineId == id)
        .Include(join => join.Engineer)
        .ToList()
        .OrderBy(entry => entry.Engineer.LastName)
        .ThenBy(entry => entry.Engineer.FirstName);
      // IEnumerable<Engineer> engineerList = engineerListQuery
      //   .ToList()
      //   .OrderBy(eng => eng.LastName)
      //   .ThenBy(eng => eng.FirstName);
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
      return View("Index");
    }
  }
}