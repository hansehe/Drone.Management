using System;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Entities
{
    public class Identity : IIdentity
    {
        public Guid Id { get; set; }

        public Identity(Guid id)
        {
            Id = id;
        }

        public static IIdentity CreateIdentity()
        {
            var identity = new Identity(Guid.NewGuid());
            return identity;
        }
    }
}