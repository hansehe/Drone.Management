using System;

namespace Drone.Management.Entities.Interfaces
{
    public interface IIdentity
    {
        Guid Id { get; set; }
    }
}