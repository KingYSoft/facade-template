using Abp.Application.Navigation;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Navigation
{
    public class MyNavigationProvider : NavigationProvider
    {
        /// <summary>
        /// 页面设置了权限，则会根据登入用户的权限，会自动过滤菜单，
        /// 返回给前端的菜单，再生成动态路由和动态菜单
        /// </summary>
        /// <param name="context"></param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            //context.Manager.MainMenu.AddItem();
        }
    }
}
