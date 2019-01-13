using Monopoly.Model.Abstract;
using Monopoly.Model.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Dialogs
{
    class ChooseTradeOpponentDialogViewModel : BindableBase
    {

        #region Constructors

        public ChooseTradeOpponentDialogViewModel(ObservableCollection<AbstractPlayer> players, Action<AbstractPlayer> closeAction)
        {
            this.Title = "Choose your trade opponent";
            _closeAction = closeAction;
            this.Players = new ObservableCollection<PlayerViewModel>(players.Select(ap => new PlayerViewModel(ap, 1)));
            foreach (PlayerViewModel player in this.Players)
            {
                player.clickAction = obj => { this.ExecuteChooseCommand(obj as PlayerViewModel); };
            }
        }

        #endregion

        #region Commands

        private DelegateCommand<PlayerViewModel> _chooseCommand;
        public DelegateCommand<PlayerViewModel> ChooseCommand =>
            _chooseCommand ?? (_chooseCommand = new DelegateCommand<PlayerViewModel>(ExecuteChooseCommand));

        void ExecuteChooseCommand(PlayerViewModel player)
        {
            _closeAction?.Invoke(player?.Player ?? null);
        }

        #endregion

        #region Properties

        public ObservableCollection<PlayerViewModel> Players { get; private set; }
        public string Title { get; private set; }

        #endregion

        #region Fields

        private Action<AbstractPlayer> _closeAction;

        #endregion

    }
}
