using System.Collections.Generic;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public ICollection<EngineerMachine> Engineers { get; set; }

    public Machine()
    {
      this.Engineers = new HashSet<EngineerMachine>();
    }
  }
}