using System.Collections.Generic;

namespace Drone.Management.Repository.Commons
{
    public class DapperInitializer : IDapperInitializer
    {
        private readonly IEnumerable<ITypeHandler> TypeHandlersField;

        private static bool DapperIsInitialized = false;

        public DapperInitializer(IEnumerable<ITypeHandler> typeHandlers)
        {
            TypeHandlersField = typeHandlers;
        }

        public void InitializeDapper()
        {
            if (DapperIsInitialized)
            {
                return;
            }
            DapperIsInitialized = true;
            foreach (var typeHandler in TypeHandlersField)
            {
                typeHandler.RegisterTypeHandler();
            }
        }
    }
}
