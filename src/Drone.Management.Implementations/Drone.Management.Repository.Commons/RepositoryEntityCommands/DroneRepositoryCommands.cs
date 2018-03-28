namespace Drone.Management.Repository.Commons.RepositoryEntityCommands
{
    public class DroneRepositoryCommands : RepositoryCommands, IDroneRepositoryCommands
    {
        public string ReadDroneIds { get; set; }

        public static void AssertDroneRepositoryCommandsAreSet(IDroneRepositoryCommands droneRepositoryCommands)
        {
            AssertRepositoryCommandsAreSet(droneRepositoryCommands);
            AssertObjectIsNotNull(droneRepositoryCommands.ReadDroneIds, "droneRepositoryCommands.ReadDroneIds is null");
        }
    }
}
