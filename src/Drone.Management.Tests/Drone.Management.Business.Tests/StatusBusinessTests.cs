using System;
using System.Threading.Tasks;
using Drone.Management.Business.Interfaces;
using Drone.Management.Entities.Interfaces;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Repository.Interfaces.RepositoryValues;
using Drone.Management.Settings.Interfaces;
using Moq;
using Xunit;

namespace Drone.Management.Business.Tests
{
    public class StatusBusinessTests
    {
        public static IIdentity GetIdentity()
        {
            var id = new Mock<IIdentity>();
            id.Setup(x => x.Id).Returns(Guid.NewGuid());
            return id.Object;
        }

        public static IStatus GetStatus()
        {
            var statusMock = new Mock<IStatus>();
            statusMock.Setup(x => x.Id).Returns(GetIdentity().Id);
            statusMock.Setup(x => x.Created).Returns(DateTime.Now);
            statusMock.Setup(x => x.Status).Returns("mocked status");
            statusMock.Setup(x => x.Operational).Returns(true);
            return statusMock.Object;
        }

        public static IRepositoryBuilder GetRepositoryBuilder()
        {
            var repositoryBuilderMock = new Mock<IRepositoryBuilder>();
            repositoryBuilderMock.Setup(x => x.BuildStatusRepository(It.IsAny<ISettings>()))
                .Returns(new Mock<IStatusRepository>().Object);
            return repositoryBuilderMock.Object;
        }

        public static IStatusBusiness GetStatusBusiness()
        {
            var business = new StatusBusiness(GetRepositoryBuilder(), new Mock<ISettings>().Object);
            return business;
        }
        
        [Fact]
        public async Task CreateStatus_Success()
        {
            var business = GetStatusBusiness();
            await business.CreateStatus(GetStatus());
        }

        [Fact]
        public async Task GetStatus_Success()
        {
            var business = GetStatusBusiness();
            await business.GetStatus(GetIdentity());
        }

        [Fact]
        public async Task GetStatusIds_Success()
        {
            var business = GetStatusBusiness();
            await business.GetStatusIds(GetIdentity());
        }
    }
}
