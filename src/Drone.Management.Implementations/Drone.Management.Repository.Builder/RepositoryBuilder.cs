using System;
using System.Collections.Generic;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.Builder
{
    public class RepositoryBuilder : IRepositoryBuilder
    {
        private readonly IEnumerable<IDroneRepository> DroneRepositoriesField;
        private readonly IEnumerable<IStatusRepository> StatusRepositoriesField;

        public RepositoryBuilder(
            IEnumerable<IDroneRepository> droneRepositories,
            IEnumerable<IStatusRepository> statusRepositories)
        {
            DroneRepositoriesField = droneRepositories;
            StatusRepositoriesField = statusRepositories;
        }

        public IDroneRepository BuildDroneRepository(ISettings settings)
        {
            foreach (var droneRepositoryResolver in DroneRepositoriesField)
            {
                if (droneRepositoryResolver.CanUseRepository(settings))
                {
                    droneRepositoryResolver.SetupRepository(settings);
                    return droneRepositoryResolver;
                }
            }
            throw new Exception("Cannot resolve drone repository!");
        }

        public IStatusRepository BuildStatusRepository(ISettings settings)
        {
            foreach (var statusRepositoryResolver in StatusRepositoriesField)
            {
                if (statusRepositoryResolver.CanUseRepository(settings))
                {
                    statusRepositoryResolver.SetupRepository(settings);
                    return statusRepositoryResolver;
                }
            }
            throw new Exception("Cannot resolve status repository!");
        }
    }
}
