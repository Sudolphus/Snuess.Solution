using System;
using System.Collections.Generic;

namespace Factory.Models
{
  public class Machine : IComparable<Machine>
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public ICollection<EngineerMachine> Engineers { get; set; }

    public Machine()
    {
      this.Engineers = new HashSet<EngineerMachine>();
    }

    public int CompareTo(Machine otherMachine)
    {
      if (otherMachine == null)
      {
        return 1;
      }
      return this.Name.CompareTo(otherMachine.Name);
    }
  }
}