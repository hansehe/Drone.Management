namespace Drone.Management.Repository.Commons.RepositoryEntityCommands
{
    public interface IDroneRepositoryCommands : IRepositoryCommands
    {
        string ReadDroneIds { get; }
    }
}
