using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace Drone.Management.Repository.Commons
{
    public interface ISqlRepository
    {
        void SetupRepository(AvailableDbConnections dbConnectionType, IRepositoryCommands repositoryCommands);

        Task<IEnumerable<T>> GetObjects<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null);

        Task<T> GetObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null);

        Task CreateObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null);

        Task UpdateObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null);

        Task DeleteObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null);
    }
}
