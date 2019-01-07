using Monopoly.UserField.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Monopoly.UserField
{
    public class UserFieldModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("PlayerInfoRegion", typeof(Field));
            regionManager.RegisterViewWithRegion("PlayerDetailsRegion", typeof(PlayerDetails));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}