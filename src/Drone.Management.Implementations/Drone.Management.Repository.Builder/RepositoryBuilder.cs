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

        public RepositoryBuilder(
            IEnumerable<IDroneRepository> droneRepositories)
        {
            DroneRepositoriesField = droneRepositories;
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

            throw new Exception("Cannot resolve event repository!");
        }
    }
}
