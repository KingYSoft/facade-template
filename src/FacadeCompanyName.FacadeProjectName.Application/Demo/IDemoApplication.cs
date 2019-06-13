using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Demo
{
    public interface IDemoApplication : IFacadeProjectNameApplicationBase
    {
        Task<string> Check(CheckInput input);
    }
}
