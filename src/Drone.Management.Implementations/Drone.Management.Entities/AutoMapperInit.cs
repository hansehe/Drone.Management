using Drone.Management.Entities.Interfaces;
using Drone.Management.Entities.Models;

namespace Drone.Management.Entities
{
    public static class AutoMapperInit
    {
        public static void AutoMapDtoModels()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IDrone, DroneDto>();
                cfg.CreateMap<IDroneStatus, DroneStatusDto>();
                cfg.CreateMap<IIdentity, IdentityDto>();
            });
        }
    }
}
