using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Monopoly.Model.Models
{
    public class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager(ICardLocator cardLocator)
        {
            _cards = cardLocator.GetCardSet();
            this.Cards = new ReadOnlyObservableCollection<AbstractCard>(_cards);
        }

        #endregion

        #region Fields

        private ObservableCollection<AbstractCard> _cards;

        public ReadOnlyObservableCollection<AbstractCard> Cards { get; protected set; }

        #endregion
    }
}
