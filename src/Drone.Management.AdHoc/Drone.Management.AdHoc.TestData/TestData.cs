using System;
using System.Collections.Generic;
using System.Globalization;
using Drone.Management.Entities.Interfaces;

namespace Drone.Management.AdHoc.TestData
{
    public class TestData : ITestData
    {
        public IList<Tuple<IDrone, IDroneStatus>> DataSets { get; } = new List<Tuple<IDrone, IDroneStatus>>();

        public void GenerateDataSets(long nDatas)
        {
            DataSets.Clear();
            for (long i = 0; i < nDatas; i++)
            {
                var dataSet = GenerateDataSet(i.ToString());
                DataSets.Add(dataSet);
            }
        }

        private static Tuple<IDrone, IDroneStatus> GenerateDataSet(string tag)
        {
            var drone = GetDrone(tag);
            var droneStatus = GetDroneStatus(tag, drone);
            var dataSet = new Tuple<IDrone, IDroneStatus>(drone, droneStatus);
            return dataSet;
        }

        private static IDrone GetDrone(string tag)
        {
            var timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            tag = $"Drone {tag} - {timestamp}";
            var owner = $"Drone owner {tag}";
            var drone = Entities.Drone.CreateDrone(tag, owner);
            return drone;
        }

        private static IDroneStatus GetDroneStatus(string tag, IIdentity droneId)
        {
            var timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var status = $"Drone status {tag} - {timestamp}";
            var droneStatus = Entities.DroneStatus.CreateDroneStatus(status, true, droneId.Id);
            return droneStatus;
        }
    }
}
