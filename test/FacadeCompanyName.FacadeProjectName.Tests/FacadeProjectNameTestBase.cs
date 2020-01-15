using Abp.Dependency;
using Abp.TestBase;

namespace FacadeCompanyName.FacadeProjectName.Tests
{
    public abstract class FacadeProjectNameTestBase : AbpIntegratedTestBase<FacadeProjectNameTestModule>
    {
        protected FacadeProjectNameTestBase()
            : base(true, IocManager.Instance)
        {
            AbpSession.TenantId = 1;
            AbpSession.UserId = 10000;
        }
    }
}
