using FacadeCompanyName.FacadeProjectName.Application.Health;
using FacadeCompanyName.FacadeProjectName.DomainService.Share.App;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
namespace FacadeCompanyName.FacadeProjectName.Tests.Health
{
    public class HealthApplication_Tests
    {
        [Fact]
        public async Task Check_Test()
        {
            var oracleRepository = new Mock<IAppOracleRepository>();
            var sqlServerRepository = new Mock<IAppSqlServerRepository>();
            var mySqlRepository = new Mock<IAppMySqlRepository>();
            var expectedTimestamp = "2024-01-01 00:00:00";

            oracleRepository
                .Setup(repo => repo.ExecuteScalarAsync<string>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedTimestamp);

            var healthApplication = new HealthApplication(
                oracleRepository.Object,
                sqlServerRepository.Object,
                mySqlRepository.Object);

            // Act
            var output = await healthApplication.Check();

            // Assert
            output.ShouldBe(expectedTimestamp);
            oracleRepository.Verify(
                repo => repo.ExecuteScalarAsync<string>(
                    It.Is<string>(query => query.Contains("sysdate")),
                    It.IsAny<object>()),
                Times.Once);
        }
    }
}
