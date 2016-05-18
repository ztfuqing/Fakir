using Abp.Application.Navigation;
using Abp.Localization;

namespace Fakir.Web
{
    public class FrontEndNavigationProvider : NavigationProvider
    {
        public const string MenuName = "Frontend";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var frontEndMenu = new MenuDefinition(MenuName, new FixedLocalizableString("Frontend menu"));
            context.Manager.Menus[MenuName] = frontEndMenu;

            frontEndMenu

                .AddItem(new MenuItemDefinition(
                    "Home",
                    L("HomePage"),
                    url: ""
                    )

                //ABOUT
                ).AddItem(new MenuItemDefinition(
                    "About",
                    L("AboutUs"),
                    url: "About"
                    )

                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FakirConsts.LocalizationSourceName);
        }
    }
}