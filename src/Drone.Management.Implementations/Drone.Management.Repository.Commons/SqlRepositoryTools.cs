using System.Data;
using Dapper;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Repository.Commons
{
    internal static class SqlRepositoryTools
    {
        internal static SqlMapper.IDynamicParameters ConvertIdentityToDynamicParameters(IIdentity id)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ID", id.Id, DbType.Guid);
            return dynamicParameters;
        }
    }
}
