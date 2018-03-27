using System;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Entities.Models
{
    public class IdentityDto : IIdentity
    {
        public Guid Id { get; set; }

        public static IdentityDto CreateIdentityDto(string guid)
        {
            var idDto = new IdentityDto
            {
                Id = Guid.Parse(guid)
            };
            return idDto;
        }
    }
}