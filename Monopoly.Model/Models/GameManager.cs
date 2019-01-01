using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Monopoly.Model.Models
{
    class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager(ICardLocator cardLocator)
        {
            _cards = cardLocator.GetCardSet();
        }

        #endregion

        #region Fields

        private ObservableCollection<AbstractCard> _cards;

        public ReadOnlyObservableCollection<AbstractCard> Cards { get; protected set; }

        #endregion
    }
}
