using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Drone.Management.Repository.Commons
{
    public class SqlRepository : ISqlRepository
    {
        private readonly IDbConnectionWrapper DbConnectionWrapperField;

        public SqlRepository(IDbConnectionWrapper dbConnectionWrapper)
        {
            DbConnectionWrapperField = dbConnectionWrapper;
        }

        public void SetupRepository(AvailableDbConnections dbConnectionType, IRepositoryCommands repositoryCommands)
        {
            DbConnectionWrapperField.SetupRepository(dbConnectionType, repositoryCommands);
        }

        public Task<IEnumerable<T>> GetObjects<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null)
        {
            return DbConnectionWrapperField.DbQuery<T>(sqlCommand, dynamicParameters);
        }

        public async Task<T> GetObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null)
        {
            return (await DbConnectionWrapperField.DbQuery<T>(sqlCommand, dynamicParameters)).FirstOrDefault();
        }

        public Task CreateObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null)
        {
            return DbConnectionWrapperField.DbExecute<T>(sqlCommand, dynamicParameters);
        }

        public Task UpdateObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null)
        {
            return DbConnectionWrapperField.DbExecute<T>(sqlCommand, dynamicParameters);
        }

        public Task DeleteObject<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters = null)
        {
            return DbConnectionWrapperField.DbExecute<T>(sqlCommand, dynamicParameters);
        }
    }
}
