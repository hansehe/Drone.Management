using System;
using System.Data;
using Dapper;
using Drone.Management.Entities;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.Repository.Commons.TypeValues
{
    public class IdentityHandler : SqlMapper.TypeHandler<IIdentity>, ITypeHandler
    {
        public override void SetValue(IDbDataParameter parameter, IIdentity value)
        {
            parameter.Value = value.Id;
        }

        public override IIdentity Parse(object value)
        {
            return new Identity((Guid)value);
        }

        public void RegisterTypeHandler()
        {
            SqlMapper.AddTypeHandler(new IdentityHandler());
        }
    }
}
