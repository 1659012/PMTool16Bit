using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace PMTool16Bit.Localization
{
    public static class PMTool16BitLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(PMTool16BitConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(PMTool16BitLocalizationConfigurer).GetAssembly(),
                        "PMTool16Bit.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
