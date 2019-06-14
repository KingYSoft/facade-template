using FacadeCompanyName.FacadeProjectName.Application.Demo;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
namespace FacadeCompanyName.FacadeProjectName.Tests.Demo
{
    public class DemoApplication_Tests : FacadeProjectNameTestBase
    {
        private readonly IDemoApplication _demoApplication;
        public DemoApplication_Tests()
        {
            _demoApplication = Resolve<IDemoApplication>();
        }

        [Fact]
        public async Task Check_Test()
        {
            // Act
            var output = await _demoApplication.Check(new DomainService.Demo.Dto.CheckInput
            {
                Id = 1
            });

            // Assert
            output.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
