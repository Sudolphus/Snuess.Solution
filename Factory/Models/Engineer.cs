using System.Collections.Generic;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<EngineerMachine> EngineersMachines { get; set; }

    public Engineer()
    {
      this.EngineersMachiens = new HashSet<EngineerMachine>();
    }
  }
}