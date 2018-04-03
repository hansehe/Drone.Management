using System;

namespace Drone.Management.Entities.Interfaces
{
    public interface IBase : IIdentity
    {
        DateTime Created { get; set; }
    }
}
