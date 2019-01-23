using Monopoly.GameField.Helpers;
using Monopoly.GameField.ViewModels;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Monopoly.GameField.Views
{
    /// <summary>
    /// Логика взаимодействия для ViewA.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public Field()
        {
            InitializeComponent();
            //((INotifyCollectionChanged)this.Board.ItemsSource).CollectionChanged += this.PlayersCollectionChanged;
        }

        //private void PlayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (ContentPresenter item in e.NewItems)
        //        {
        //            var behColl = Interaction.GetBehaviors(item);
        //            behColl.Add(new PlayerViewModelBehavior());
        //        }
        //    }
        //}
    }
}
