using Monopoly.Model.Abstract;
using Monopoly.Model.Events;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.UserField.ViewModels
{
    class PlayerDetailsViewModel : BindableBase
    {
        #region Constructors

        public PlayerDetailsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ShowPlayerDetailEvent>().Subscribe(this.OnPlayerDetailShow);
            this.RealtyCards = new ObservableCollection<AbstractCard>();
            this.ActionCards = new ObservableCollection<AbstractCard>();
        }

        #endregion

        #region Methods

        private void OnPlayerDetailShow(AbstractPlayer player)
        {
            this.Player = player;
            this.RealtyCards = this.Player.RealtyCards;
            this.ActionCards = this.Player.ActionCards;
        }

        #endregion

        #region Properties

        private AbstractPlayer _player;
        public AbstractPlayer Player
        {
            get { return _player; }
            set { SetProperty(ref _player, value); }
        }

        private ObservableCollection<AbstractCard> _realtyCards;
        public ObservableCollection<AbstractCard> RealtyCards
        {
            get { return _realtyCards; }
            set { SetProperty(ref _realtyCards, value); }
        }

        private ObservableCollection<AbstractCard> _actionCards;
        public ObservableCollection<AbstractCard> ActionCards
        {
            get { return _actionCards; }
            set { SetProperty(ref _actionCards, value); }
        }

        #endregion
    }
}
