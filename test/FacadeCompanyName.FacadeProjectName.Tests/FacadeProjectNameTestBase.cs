using Abp.Dependency;
using Abp.TestBase;
using Castle.Core.Logging;

namespace FacadeCompanyName.FacadeProjectName.Tests
{
    public abstract class FacadeProjectNameTestBase : AbpIntegratedTestBase<FacadeProjectNameTestModule>
    {
        public ILogger Logger { get; set; }
        protected FacadeProjectNameTestBase()
            : base(true, IocManager.Instance)
        {
            AbpSession.TenantId = 1;
            AbpSession.UserId = 10000;
            Logger = Resolve<ILogger>();
        }
    }
}
