using Monopoly.Model.Abstract;
using Monopoly.Model.Events;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using Monopoly.Model.ViewModels;
using Prism.Commands;
using Prism.Events;
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

        public FieldViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.Message = "Hello from view model!";
            this.GameManager = (IGameManager)regionManager.Regions["GameFieldRegion"].Context;

            this.Cards = new ObservableCollection<CardViewModel>(this.GameManager.Cards.Select(c => new CardViewModel(c, this.GameManager)));
            Watch(this.GameManager.Cards, this.Cards, cvm => cvm.Card);

            int order = 1;
            this.Players = new ObservableCollection<PlayerViewModel>(this.GameManager.Players.Select(p => new PlayerViewModel(p, order++)));
            //Watch(this.GameManager.Players, this.Players, cvm => cvm.Card);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ShowAvailableForBuildingTowns>().Subscribe(this.ShowAvailableForBuildingTowns);
            _eventAggregator.GetEvent<StopShowAvailableForBuildingTowns>().Subscribe(this.StopShowAvailableForBuildingTowns);
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

        private void ShowAvailableForBuildingTowns()
        {
            var availableForBuildingTowns = this.Cards.Where(cvm => (cvm.Card != null && cvm.Owner == this.Players[this.GameManager.CurrentPlayer].Player
                                                                      && cvm.Card is TownCard) && cvm.CardGroup.IsMonopoly);
            foreach (CardViewModel cvm in availableForBuildingTowns)
            {
                cvm.onClickAction = () =>
              {
                  _gameManager.BuildHouse(cvm.Card);
              };
            }

            var unavailableTowns = this.Cards.Except(availableForBuildingTowns);
            foreach (CardViewModel cvm in unavailableTowns)
            {
                cvm.Gray = true;
            }
        }

        private void StopShowAvailableForBuildingTowns()
        {
            var availableForBuildingTowns = this.Cards.Where(cvm => (cvm.Card != null && cvm.Owner == this.Players[this.GameManager.CurrentPlayer].Player
                                                                      && cvm.Card is TownCard) && cvm.CardGroup.IsMonopoly);
            foreach (CardViewModel cvm in availableForBuildingTowns)
            {
                cvm.onClickAction = null;
            }

            var unavailableTowns = this.Cards.Except(availableForBuildingTowns);
            foreach (CardViewModel cvm in unavailableTowns)
            {
                cvm.Gray = false;
            }
        }
        #endregion

        #region Commands

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
        public ObservableCollection<PlayerViewModel> Players { get; private set; }

        private IEventAggregator _eventAggregator;

        #endregion

    }
}
