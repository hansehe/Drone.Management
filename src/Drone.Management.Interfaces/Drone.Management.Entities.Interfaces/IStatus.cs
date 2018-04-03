using System;

namespace Drone.Management.Entities.Interfaces
{
    public interface IStatus : IBase
    {
        string Status { get; set; }

        bool Operational { get; set; }

        Guid DroneId { get; set; }
    }
}
