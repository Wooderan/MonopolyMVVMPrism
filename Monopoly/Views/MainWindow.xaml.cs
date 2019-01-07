using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Monopoly.ViewModels;
using System.Windows;

namespace Monopoly.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainWindowViewModel).DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
