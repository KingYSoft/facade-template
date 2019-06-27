using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;
namespace FacadeCompanyName.FacadeProjectName.DomainService.Localization
{
    public static class FacadeProjectNameLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(FacadeProjectNameConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(FacadeProjectNameLocalizationConfigurer).GetAssembly(),
                        "FacadeCompanyName.FacadeProjectName.DomainService.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
