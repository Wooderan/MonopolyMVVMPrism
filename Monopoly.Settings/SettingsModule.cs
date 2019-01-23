using Monopoly.Settings.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Monopoly.Settings
{
    public class SettingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("SettingsRegion", typeof(SettingsField));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}