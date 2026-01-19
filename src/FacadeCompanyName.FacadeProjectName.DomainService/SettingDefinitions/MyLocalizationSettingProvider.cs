using Abp.Configuration;
using Abp.Localization;
using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.SettingDefinitions
{
    /// <summary>
    /// 1.QueryStringRequestCultureProvider
    /// 2.AbpUserRequestCultureProvider (根据用户设置的 Abp.Localization.DefaultLanguageName)
    /// 3.AbpLocalizationHeaderRequestCultureProvider  (.AspNetCore.Culture)
    /// 4.CookieRequestCultureProvider
    /// 5.AcceptLanguageHeaderRequestCultureProvider
    /// 6.AbpDefaultRequestcultureProvider (根据应用程序设置的 Abp.Localization.DefaultLanguageName)
    /// </summary>
    public class MyLocalizationSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return
                [
                    new SettingDefinition(LocalizationSettingNames.DefaultLanguage, "en", L("DefaultLanguage"), scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider())
                ];
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AbpConsts.LocalizationSourceName);
        }
    }
}
