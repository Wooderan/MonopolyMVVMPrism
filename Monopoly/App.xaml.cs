using Monopoly.Model.Interfaces;
using Monopoly.Model.Services;
using Monopoly.Model.Models;
using Monopoly.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Unity.Injection;

namespace Monopoly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IPlayerProvider playerProvider = new MockPlayerProvider();
            containerRegistry.RegisterInstance<ICardLocator>(new BaseCardLocator());
            containerRegistry.RegisterInstance<IPlayerProvider>(playerProvider);
            ICardLocator cardLocator = this.Container.Resolve<ICardLocator>();
            containerRegistry.RegisterInstance<IDiceProvider>(new TestDiceProvider());
            containerRegistry.RegisterInstance<IExchangeManager>(new ExchangeManager());
            containerRegistry.RegisterInstance<IGameManager>(new GameManager(cardLocator, playerProvider, this.Container.Resolve<IDiceProvider>()));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
//TODO
//add player instance+
//add player provider+
//add player support to game manager+
//add player view model+
//add viewModel support to userfield VM
//add player template
