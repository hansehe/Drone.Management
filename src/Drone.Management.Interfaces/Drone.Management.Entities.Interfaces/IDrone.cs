using System;

namespace Drone.Management.Entities.Interfaces
{
    public interface IDrone : IBase
    {
        DateTime Updated { get; set; }

        string Tag { get; set; }

        string Owner { get; set; }
    }
}
