namespace Drone.Management.Repository.Commons.RepositoryCommandValues
{
    public interface IDroneRepositoryCommands : IRepositoryCommands
    {
        string ReadDroneIds { get; }
    }
}
