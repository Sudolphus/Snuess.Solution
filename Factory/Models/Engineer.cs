using System;
using System.Collections.Generic;

namespace Factory.Models
{
  public class Engineer : IComparable<Engineer>
  {
    public int EngineerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public ICollection<EngineerMachine> Machines { get; set; }

    public Engineer()
    {
      this.Machines = new HashSet<EngineerMachine>();
    }
    
    public int CompareTo(Engineer otherEngineer)
    {
      if (otherEngineer == null)
      {
        return 1;
      }
      if (this.LastName != otherEngineer.LastName)
      {
        return this.LastName.CompareTo(otherEngineer.LastName);
      }
      else
      {
        return this.FirstName.CompareTo(otherEngineer.FirstName);
      }
    }
  }
}