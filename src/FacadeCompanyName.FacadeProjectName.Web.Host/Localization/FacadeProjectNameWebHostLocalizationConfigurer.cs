using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Reflection.Extensions;
using Abp.Web;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Localization
{
    public class FacadeProjectNameWebHostLocalizationConfigurer
    {

        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Extensions.Add(
               new LocalizationSourceExtensionInfo(AbpWebConsts.LocalizaionSourceName,
                   new XmlEmbeddedFileLocalizationDictionaryProvider(
                       typeof(FacadeProjectNameWebHostLocalizationConfigurer).GetAssembly(),
                       "FacadeCompanyName.FacadeProjectName.Web.Host.Localization.AbpWebSourceFiles"
                   )
               )
           );
        }
    }
}
