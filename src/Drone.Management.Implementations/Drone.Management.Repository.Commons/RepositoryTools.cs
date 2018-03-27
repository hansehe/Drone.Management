using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Drone.Management.Commons;
using Drone.Management.Repository.Commons.RepositoryCommandValues;
using Npgsql;

namespace Drone.Management.Repository.Commons
{
    public static class RepositoryTools
    {
        public static IRepositoryCommands LoadAndAssertRepositorySqlCommands(string sqlCommandsResourcePath, Assembly callingAssembly)
        {
            var repositorySqlCommands = JsonParser.GetDeserializedObjectFromResource<RepositoryCommands>(sqlCommandsResourcePath, callingAssembly);
            RepositoryCommands.AssertRepositoryCommandsAreSet(repositorySqlCommands);
            return repositorySqlCommands;
        }

        public static IDroneRepositoryCommands LoadAndAssertDroneRepositorySqlCommands(string sqlCommandsResourcePath, Assembly callingAssembly)
        {
            var repositorySqlCommands = JsonParser.GetDeserializedObjectFromResource<DroneRepositoryCommands>(sqlCommandsResourcePath, callingAssembly);
            DroneRepositoryCommands.AssertDroneRepositoryCommandsAreSet(repositorySqlCommands);
            return repositorySqlCommands;
        }

        public static IDbConnection ResolveDbConnection(AvailableDbConnections dbConnection)
        {
            switch (dbConnection)
            {
                case AvailableDbConnections.SqlConnection:
                    return new SqlConnection();
                case AvailableDbConnections.NpgsqlConnection:
                    return new NpgsqlConnection();
                default:
                    throw new ArgumentException($"Unknown dbconnection request: {dbConnection}");
            }
        }
    }
}
