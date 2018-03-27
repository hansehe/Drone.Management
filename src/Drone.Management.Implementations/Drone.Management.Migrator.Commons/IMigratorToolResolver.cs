namespace Drone.Management.Migrator.Commons
{
    public interface IMigratorToolResolver
    {
        IMigratorTool ResolveMigratorTool(AvailableMigratorTools migratorTool);
    }
}
