namespace Drone.Management.Repository.Commons.RepositoryEntityCommands
{
    public class StatusRepositoryCommands : RepositoryCommands, IStatusRepositoryCommands
    {
        public string ReadStatusIds { get; set; }

        public static void AssertStatusRepositoryCommandsAreSet(IStatusRepositoryCommands statusRepositoryCommands)
        {
            AssertRepositoryCommandsAreSet(statusRepositoryCommands);
            AssertObjectIsNotNull(statusRepositoryCommands.ReadStatusIds, "statusRepositoryCommands.ReadStatusIds is null");
        }
    }
}
