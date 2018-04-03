using System;
using System.Collections.Generic;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.TestData
{
    public interface ITestData
    {
        IList<Tuple<IDrone, IDroneStatus>> DataSets { get; }

        void GenerateDataSets(long nDatas);
    }
}
