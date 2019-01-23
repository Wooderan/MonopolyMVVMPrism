using Monopoly.Model.Abstract;
using Monopoly.Model.Models;
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
        void Buy(AbstractPlayer winner, int cost);
        void BuildHouse(AbstractCard card);
        void DestroyHouse(AbstractCard card);
        void PledgeCard(AbstractCard card);
        void BuyFromPledgeCard(AbstractCard card);
        void ShowAuction();
        void GiveMoney(int money);
        void TakeMoney(int money);
        void ChestAction();
        void LuckyAction();
        void GameAction(GameAction action);
        void GoToJail();
        void FreeFromJail();
        void ActivateEventCard(EventCard eventCard);
        void GiveEventCard(EventCard eventCard);
        void ReloadGame();


        #endregion

        #region Event
        event Action<AbstractCard> ShowBuyOrAuctionDialog;
        event Action<AbstractRealtyCard, ObservableCollection<AbstractPlayer>> ShowAuctionDialog;
        #endregion

        #region Fields

        ReadOnlyObservableCollection<AbstractCard> Cards { get;}
        ObservableCollection<AbstractPlayer> Players { get; }
        int CurrentPlayer { get; }
        bool HaveDraws { get; }
        bool HaveNotDraws { get; }
        bool HasTowns { get;}
        bool HasMonopoly { get;}
        bool HasBuildings { get; }
        bool HasMortgageCards { get; }
        bool CanAct { get; }
        bool Animating { get; set; }

        void AfterAction();





        #endregion



    }
}
