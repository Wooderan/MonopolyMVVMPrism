using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.GameField.ViewModels
{
    class FieldViewModel : BindableBase
    {
        #region Constructors

        public FieldViewModel(IRegionManager regionManager)
        {
            this.Message = "Hello from view model!";
            this.GameManager = (IGameManager)regionManager.Regions["GameFieldRegion"].Context;

            this.Cards = new ObservableCollection<CardViewModel>(this.GameManager.Cards.Select(c => new CardViewModel(c, this.GameManager)));
            Watch(this.GameManager.Cards, this.Cards, cvm => cvm.Card);
        }

        #endregion

        #region Methods
        private static void Watch<T, T2>(ReadOnlyObservableCollection<T> collToWatch, ObservableCollection<T2> collToUpdate,
            Func<T2, object> modelProperty)
        {
            ((INotifyCollectionChanged)collToWatch).CollectionChanged += (s, a) =>
            {
                if (a.NewItems?.Count == 1) collToUpdate.Add((T2)Activator.CreateInstance(typeof(T2), (T)a.NewItems[0], null));
                if (a.OldItems?.Count == 1) collToUpdate.Remove(collToUpdate.First(mv => modelProperty(mv) == a.OldItems[0]));
            };
        }
        #endregion

        #region Properties
        private IGameManager _gameManager;
        public IGameManager GameManager
        {
            get { return _gameManager; }
            set { SetProperty(ref _gameManager, value); }
        }

        private string _message = "Hello from view model!";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        #region Fields

        public ObservableCollection<CardViewModel> Cards { get; private set; }

        #endregion

    }
}
