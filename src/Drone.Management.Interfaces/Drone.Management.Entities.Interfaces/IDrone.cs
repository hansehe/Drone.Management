﻿namespace Drone.Management.Entities.Interfaces
{
    public interface IDrone : IBase
    {
        string Tag { get; set; }

        string Owner { get; set; }
    }
}