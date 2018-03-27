using System;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Entities.Models
{
    public class DroneDto : IDrone
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Tag { get; set; }
    }
}
