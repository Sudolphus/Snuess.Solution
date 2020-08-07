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
  }
}