using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace Drone.Management.Repository.Commons
{
    public interface IDbConnectionWrapper : IDisposable
    {
        void SetupRepository(AvailableDbConnections dbConnection, IRepositoryCommands repositoryCommands);

        Task<IEnumerable<T>> DbQuery<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters);

        Task DbExecute<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters);
    }
}
