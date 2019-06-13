using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Demo
{
    public interface IDemoService : IFacadeProjectNameDomainServiceBase
    {
        Task<string> Check(CheckInput input);
    }
}
