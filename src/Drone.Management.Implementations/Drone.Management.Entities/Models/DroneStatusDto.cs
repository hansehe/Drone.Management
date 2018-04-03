using System;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Entities.Models
{
    public class DroneStatusDto : IDroneStatus
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
        public bool Operational { get; set; }
        public Guid DroneId { get; set; }
    }
}
