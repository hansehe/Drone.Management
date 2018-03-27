using System;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Entities
{
    public class Drone : IDrone
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Tag { get; set; }

        public static IDrone CreateDrone(
            string tag)
        {
            var timestamp = DateTime.Now;
            var drone = new Drone
            {
                Id = Identity.CreateIdentity().Id,
                Tag = tag,
                Created = timestamp,
                Updated = timestamp
            };
            return drone;
        }
    }
}
