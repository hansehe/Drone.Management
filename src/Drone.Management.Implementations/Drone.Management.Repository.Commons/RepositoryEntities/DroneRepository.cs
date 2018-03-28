using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Commons.RepositoryEntityCommands;
using Drone.Management.Repository.Interfaces.RepositoryValues;

namespace Drone.Management.Repository.Commons.RepositoryEntities
{
    public abstract class DroneRepository<T> : RepositoryBase<T>, IDroneRepository
    {
        private readonly IDroneRepositoryCommands DroneRepositoriesCommandsField;

        protected DroneRepository(ISqlRepository repository, IDapperInitializer dapperInitializer, Assembly callingAssembly) : base(repository, dapperInitializer, callingAssembly)
        {
            DroneRepositoriesCommandsField = RepositoryTools.LoadAndAssertDroneRepositorySqlCommands(SqlCommandsResourcePath, callingAssembly);
        }

        public Task CreateDrone(IDrone drone)
        {
            var dynamicParameters = ConvertDroneToDynamicParameters(drone);
            return RepositoryField.CreateObject<Entities.Drone>(DroneRepositoriesCommandsField.CreateObject, dynamicParameters);
        }

        public Task<IEnumerable<IIdentity>> ReadDroneIds()
        {
            return RepositoryField.GetObjects<IIdentity>(DroneRepositoriesCommandsField.ReadDroneIds);
        }

        public async Task<IDrone> ReadDrone(IIdentity id)
        {
            var dynamicParameters = SqlRepositoryTools.ConvertIdentityToDynamicParameters(id);
            return await RepositoryField.GetObject<Entities.Drone>(DroneRepositoriesCommandsField.ReadObject, dynamicParameters);
        }

        public Task UpdateDrone(IDrone drone)
        {
            var dynamicParameters = ConvertDroneToDynamicParameters(drone);
            return RepositoryField.UpdateObject<Entities.Drone>(DroneRepositoriesCommandsField.UpdateObject, dynamicParameters);
        }

        public Task DeleteDrone(IIdentity id)
        {
            var dynamicParameters = SqlRepositoryTools.ConvertIdentityToDynamicParameters(id);
            return RepositoryField.DeleteObject<Entities.Drone>(DroneRepositoriesCommandsField.DeleteObject, dynamicParameters);
        }

        private static SqlMapper.IDynamicParameters ConvertDroneToDynamicParameters(IDrone drone)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ID", drone.Id, DbType.Guid);
            dynamicParameters.Add("CREATED", drone.Created, DbType.DateTime);
            dynamicParameters.Add("UPDATED", drone.Updated, DbType.DateTime);
            dynamicParameters.Add("TAG", drone.Tag, DbType.String);
            dynamicParameters.Add("OWNER", drone.Owner, DbType.String);
            return dynamicParameters;
        }
    }
}
