using Drone.Management.Commons.Interfaces;

namespace Drone.Management.Repository.Commons
{
    public interface IRepositoryCommands : IBasicSqlCommands
    {
        string CreateObject { get; }

        string ReadObject { get; }
        
        string UpdateObject { get; }

        string DeleteObject { get; }
    }
}
