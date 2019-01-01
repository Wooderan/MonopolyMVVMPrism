using Monopoly.GameField.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Monopoly.GameField
{
    public class GameFieldModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("GameFieldRegion", typeof(Field));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}