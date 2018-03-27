using System;

namespace Drone.Management.Repository.Commons
{
    public class RepositoryCommands : IRepositoryCommands
    {
        public string Connect { get; set; }
        public string CreateObject { get; set; }
        public string ReadObject { get; set; }
        public string UpdateObject { get; set; }
        public string DeleteObject { get; set;  }

        public static void AssertRepositoryCommandsAreSet(IRepositoryCommands repositoryCommands)
        {
            AssertObjectIsNotNull(repositoryCommands.Connect, "repositoryCommands.Connect is null");
            AssertObjectIsNotNull(repositoryCommands.CreateObject, "repositoryCommands.CreateObject is null");
            AssertObjectIsNotNull(repositoryCommands.ReadObject, "repositoryCommands.ReadObject is null");
            AssertObjectIsNotNull(repositoryCommands.UpdateObject, "repositoryCommands.UpdateObject is null");
            AssertObjectIsNotNull(repositoryCommands.DeleteObject, "repositoryCommands.DeleteObject is null");
        }

        protected static void AssertObjectIsNotNull(object @object, string errorMsg)
        {
            if (@object == null)
            { 
                throw new ArgumentNullException(errorMsg);
            }
        }
    }
}
