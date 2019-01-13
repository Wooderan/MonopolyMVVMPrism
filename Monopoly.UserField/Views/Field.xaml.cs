using Monopoly.Model.ViewModels;
using Monopoly.UserField.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monopoly.UserField.Views
{
    /// <summary>
    /// Логика взаимодействия для Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public Field()
        {
            InitializeComponent();
        }

        // i used code behind because i need to get item
        private void PlayerClicked(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lvi = sender as ListViewItem;
            PlayerViewModel pvm = lvi.DataContext as PlayerViewModel;
            if (pvm != null)
            {
                (this.DataContext as FieldViewModel).ShowPlayerDetailsCommand.Execute(pvm.Player);
            }
        }

        private void onBuildHouseButton_Click(object sender, RoutedEventArgs e)
        {
            this.BuildHouseButton.Visibility = Visibility.Collapsed;
            this.StopBuildHouseButton.Visibility = Visibility.Visible;
        }

        private void onStopBuildHouseButton_Click(object sender, RoutedEventArgs e)
        {
            this.StopBuildHouseButton.Visibility = Visibility.Collapsed;
            this.BuildHouseButton.Visibility = Visibility.Visible;
        }

        private void onDestroyHouseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DestroyHouseButton.Visibility = Visibility.Collapsed;
            this.StopDestroyHouseButton.Visibility = Visibility.Visible;
        }

        private void onStopDestroyHouseButton_Click(object sender, RoutedEventArgs e)
        {
            this.StopDestroyHouseButton.Visibility = Visibility.Collapsed;
            this.DestroyHouseButton.Visibility = Visibility.Visible;
        }

        private void onMortgageButton_Click(object sender, RoutedEventArgs e)
        {
            this.MortgageButton.Visibility = Visibility.Collapsed;
            this.StopMortgageButton.Visibility = Visibility.Visible;
        }

        private void onStopMortgageButton_Click(object sender, RoutedEventArgs e)
        {
            this.StopMortgageButton.Visibility = Visibility.Collapsed;
            this.MortgageButton.Visibility = Visibility.Visible;
        }

        private void onBuyFromMortgageButton_Click(object sender, RoutedEventArgs e)
        {
            this.BuyFromMortgageButton.Visibility = Visibility.Collapsed;
            this.StopBuyFromMortgageButton.Visibility = Visibility.Visible;
        }

        private void onStopBuyFromMortgageButton_Click(object sender, RoutedEventArgs e)
        {
            this.StopBuyFromMortgageButton.Visibility = Visibility.Collapsed;
            this.BuyFromMortgageButton.Visibility = Visibility.Visible;
        }
    }
}
