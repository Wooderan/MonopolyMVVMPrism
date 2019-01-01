using Monopoly.BaseModel.Services;
using Monopoly.Infrastructure.Shared;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.BaseModel.Models
{
    class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager()
        {
            _cards = CardLocator.GetStandartCardSet();
        }

        #endregion

        #region Fields

        private ObservableCollection<ICard> _cards;

        public ReadOnlyObservableCollection<ICard> Cards { get; protected set; }

        #endregion
    }
}
