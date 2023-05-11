using FacadeCompanyName.FacadeProjectName.Application.Health;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
namespace FacadeCompanyName.FacadeProjectName.Tests.Health
{
    public class HealthApplication_Tests : FacadeProjectNameTestBase
    {
        private readonly IHealthApplication _healthApplication;
        public HealthApplication_Tests()
        {
            _healthApplication = Resolve<IHealthApplication>();
        }

        [Fact]
        public async Task Check_Test()
        {
            // Act
            var output = await _healthApplication.Check();

            // Assert
            // output.ShouldNotBeNull();
        }
    }
}
