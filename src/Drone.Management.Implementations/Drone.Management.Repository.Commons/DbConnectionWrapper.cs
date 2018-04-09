using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Drone.Management.Repository.Commons
{
    public class DbConnectionWrapper : IDbConnectionWrapper
    {
        private IDbConnection DbConnectionField;

        public void SetupRepository(AvailableDbConnections dbConnectionType, IRepositoryCommands repositoryCommands)
        {
            DbConnectionField = OpenDbConnection(dbConnectionType, repositoryCommands);
        }

        public async Task<IEnumerable<T>> DbQuery<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters)
        {
            IEnumerable<T> objects;
            if (dynamicParameters == null)
            {
                objects = DbConnectionField
                    .Query<T>(sqlCommand);
            }
            else
            {
                objects = DbConnectionField
                    .Query<T>(sqlCommand, dynamicParameters);
            }
            return objects;
        }

        public async Task DbExecute<T>(string sqlCommand, SqlMapper.IDynamicParameters dynamicParameters)
        {
            if (dynamicParameters == null)
            {
                DbConnectionField.Execute(sqlCommand);
            }
            else
            {
                DbConnectionField.Execute(sqlCommand, dynamicParameters);
            }
        }

        private static IDbConnection OpenDbConnection(AvailableDbConnections dbConnectionType, IRepositoryCommands repositoryCommands)
        {
            var dbConnection = RepositoryTools.ResolveDbConnection(dbConnectionType);
            dbConnection.ConnectionString = repositoryCommands.Connect;
            dbConnection.Open();
            return dbConnection;
        }

        public void Dispose()
        {
            DbConnectionField?.Close();
        }
    }
}
