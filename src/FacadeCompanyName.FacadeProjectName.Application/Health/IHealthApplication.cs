using System;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Application.Health
{
    public interface IHealthApplication : IFacadeProjectNameApplicationBase
    {
        Task<string> Check();
    }
}
