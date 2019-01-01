using Monopoly.Model.Interfaces;
using Monopoly.Model.Services;
using Monopoly.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

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
            containerRegistry.Register<ICardLocator, BaseCardLocator>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
