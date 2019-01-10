using Monopoly.Model.Abstract;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface IGameManager
    {
        #region Methods

        void MakeDiceRoll();
        void NextPlayer();
        void Buy();

        #endregion

        #region Event
        event Action<AbstractCard> ShowBuyOrAuctionDialog;
        #endregion

        #region Fields

        ReadOnlyObservableCollection<AbstractCard> Cards { get;}
        ObservableCollection<AbstractPlayer> Players { get; }
        int CurrentPlayer { get; }
        bool HaveDraws { get; }
        bool HaveNotDraws { get; }
        bool HasMonopoly { get;}

        void BuildHouse(AbstractCard card);


        #endregion



    }
}
