using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Monopoly.Model.Models
{
    public class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager(ICardLocator cardLocator, IPlayerProvider playerProvider)
        {
            _cards = cardLocator.GetCardSet();
            _players = playerProvider.GetPlayers();
            this.Cards = new ReadOnlyObservableCollection<AbstractCard>(_cards);
            
        }

        #endregion

        #region Commands

        #endregion

        #region Fields

        private ObservableCollection<AbstractCard> _cards;
        private ObservableCollection<AbstractPlayer> _players;

        public ReadOnlyObservableCollection<AbstractCard> Cards { get; protected set; }

        public ObservableCollection<AbstractPlayer> Players => _players;

        #endregion
    }
}
