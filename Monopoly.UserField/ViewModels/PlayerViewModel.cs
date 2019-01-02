using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Monopoly.UserField.ViewModels
{
    public class PlayerViewModel : BindableBase
    {
        #region Constructors
        public PlayerViewModel(AbstractPlayer player, IGameManager manager)
        {
            this.Player = player;
            if (this.Player != null)
            {
                this.Player.PropertyChanged += (s, e) =>
                {
                    this.RaisePropertyChanged(e.PropertyName);
                };
            }
        }
        #endregion

        #region Fields

        public AbstractPlayer Player { get; set; }

        public string Nickname => this.Player.Nickname;
        public string Avatar => this.Player.Avatar;
        public string Chip => this.Player.Chip;

        //private DelegateCommand _mockCommand;
        //public DelegateCommand MockCommand =>
        //    _mockCommand ?? (_mockCommand = new DelegateCommand(ExecuteMockCommand));

        //void ExecuteMockCommand()
        //{
        //    if (this.Player == null)
        //    {
        //        this.Player = null;
        //    }
        //}

        //bindable
        public int Money => this.Player.Money;
        public ObservableCollection<ICard> RealtyCards => this.Player.RealtyCards;
        public ObservableCollection<ICard> ActionCards => this.Player.ActionCards;

        #endregion
    }
}
