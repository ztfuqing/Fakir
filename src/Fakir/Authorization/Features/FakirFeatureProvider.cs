using Abp.Application.Features;
using Abp.Localization;
using Abp.UI.Inputs;

namespace Fakir.Authorization.Features
{
    public class FakirFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            var sampleBooleanFeature = context.Create(
                "AllowPost",
                defaultValue: "false",
                displayName: L("AllowPost"),
                inputType: new CheckboxInputType()
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FakirConsts.LocalizationSourceName);
        }
    }
}
