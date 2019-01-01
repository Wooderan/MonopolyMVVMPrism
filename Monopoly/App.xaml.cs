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
            containerRegistry.RegisterInstance<ICardLocator>(new BaseCardLocator());
            ICardLocator cardLocator = this.Container.Resolve<ICardLocator>();
            containerRegistry.RegisterInstance<IGameManager>(new GameManager(cardLocator));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
