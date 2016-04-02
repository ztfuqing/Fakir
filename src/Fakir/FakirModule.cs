using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Fakir.Authorization.Roles;
using System.Reflection;

namespace Fakir
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class FakirModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    FakirConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Fakir.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
