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
    }
}
