using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Monopoly.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class ExchangeManager : IExchangeManager
    {

        #region Constructors

        public ExchangeManager()
        {

        }

        #endregion

        #region Methods

        public void SetPlayers(AbstractPlayer playerLeft, AbstractPlayer playerRight)
        {
            PlayerLeft = playerLeft;
            PlayerRight = playerRight;
            this.Refresh();
        }

        private void Refresh()
        {
            FromPlayerLeftCards.Clear();
            FromPlayerRightCards.Clear();
            FromPlayerLeftMoney = 0;
            FromPlayerRightMoney = 0;
        }

        public void Exchange()
        {
            foreach (AbstractCard card in FromPlayerLeftCards)
            {
                PlayerLeft.ThrowCard(card);
                PlayerRight.PickUpCard(card);
            }
            foreach (AbstractCard card in FromPlayerRightCards)
            {
                PlayerRight.ThrowCard(card);
                PlayerLeft.PickUpCard(card);
            }
            int delay = 0;
            if (FromPlayerLeftMoney > 0)
            {
                PlayerLeft.ThrowMoney(FromPlayerLeftMoney, delay);
                PlayerRight.PickUpMoney(FromPlayerLeftMoney, delay);
                delay = 3;
            }
            if (FromPlayerRightMoney > 0)
            {
                PlayerRight.ThrowMoney(FromPlayerRightMoney, delay);
                PlayerLeft.PickUpMoney(FromPlayerRightMoney, delay);
            }
            this.Refresh();
        }

        #endregion

        #region Properties

        public ObservableCollection<AbstractCard> FromPlayerLeftCards { get; } = new ObservableCollection<AbstractCard>();
        public ObservableCollection<AbstractCard> FromPlayerRightCards { get; } = new ObservableCollection<AbstractCard>();

        public int FromPlayerLeftMoney { get; set; }
        public int FromPlayerRightMoney { get; set; }

        public AbstractPlayer PlayerLeft { get; private set; }
        public AbstractPlayer PlayerRight { get; private set; }

        #endregion

        #region Field


        #endregion

    }
}
