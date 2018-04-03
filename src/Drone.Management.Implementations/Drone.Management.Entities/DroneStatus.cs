using System;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Entities
{
    public class DroneStatus : IDroneStatus
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
        public bool Operational { get; set; }
        public Guid DroneId { get; set; }

        public static IDroneStatus CreateDroneStatus(
            string status,
            bool operational,
            Guid droneId)
        {
            var timestamp = DateTime.Now;
            var droneStatus = new DroneStatus
            {
                Id = Identity.CreateIdentity().Id,
                Created = timestamp,
                Status = status,
                Operational = operational,
                DroneId = droneId
            };
            return droneStatus;
        }
    }
}
