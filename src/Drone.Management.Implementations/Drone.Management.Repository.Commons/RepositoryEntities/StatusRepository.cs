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
    public abstract class StatusRepository<T> : RepositoryBase<T>, IStatusRepository
    {
        private readonly IStatusRepositoryCommands StatusRepositoriesCommandsField;

        protected StatusRepository(ISqlRepository repository, IDapperInitializer dapperInitializer, Assembly callingAssembly) : base(repository, dapperInitializer, callingAssembly)
        {
            StatusRepositoriesCommandsField = RepositoryTools.LoadAndAssertStatusRepositorySqlCommands(SqlCommandsResourcePath, callingAssembly);
        }

        public Task CreateStatus(IStatus status)
        {
            var dynamicParameters = ConvertStatusToDynamicParameters(status);
            return RepositoryField.CreateObject<Entities.Drone>(StatusRepositoriesCommandsField.CreateObject, dynamicParameters);
        }

        public Task<IEnumerable<IIdentity>> ReadStatusIds(IIdentity droneId)
        {
            var dynamicParameters = SqlRepositoryTools.ConvertIdentityToDynamicParameters(droneId);
            return RepositoryField.GetObjects<IIdentity>(StatusRepositoriesCommandsField.ReadStatusIds, dynamicParameters);
        }

        public async Task<IStatus> ReadStatus(IIdentity id)
        {
            var dynamicParameters = SqlRepositoryTools.ConvertIdentityToDynamicParameters(id);
            return await RepositoryField.GetObject<Entities.DroneStatus>(StatusRepositoriesCommandsField.ReadObject, dynamicParameters);
        }
        private static SqlMapper.IDynamicParameters ConvertStatusToDynamicParameters(IStatus status)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ID", status.Id, DbType.Guid);
            dynamicParameters.Add("CREATED", status.Created, DbType.DateTime);
            dynamicParameters.Add("STATUS", status.Status, DbType.String);
            dynamicParameters.Add("OPERATIONAL", status.Operational, DbType.Boolean);
            dynamicParameters.Add("FK_DRONEID", status.DroneId, DbType.Guid);
            return dynamicParameters;
        }
    }
}
