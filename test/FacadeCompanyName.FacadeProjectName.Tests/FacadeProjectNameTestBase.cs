using Abp.TestBase;

namespace FacadeCompanyName.FacadeProjectName.Tests
{
    public abstract class FacadeProjectNameTestBase : AbpIntegratedTestBase<FacadeProjectNameTestModule>
    {
        protected FacadeProjectNameTestBase()
        {
            AbpSession.TenantId = 1;
            AbpSession.UserId = 10000;
        }
    }
}
