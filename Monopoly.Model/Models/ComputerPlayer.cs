using Monopoly.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class ComputerPlayer : AbstractPlayer
    {

        public ComputerPlayer(string nickname, string avatar, string chip, int money) : base()
        {
            this.Nickname = nickname;
            this.Avatar = avatar;
            this.Chip = chip;
            this.Money = money;
        }

        internal bool DecideBuyOrAuction(AbstractRealtyCard currentCard)
        {
            if (this.Money > currentCard.Cost)
            {
                return true;//buy
            }
            return false;//auction
        }

        internal void DecideAfterStep(GameManager gameManager)
        {
            gameManager.NextPlayer();
        }

        public int DecideBetOrLeave(AbstractRealtyCard card, int lastBet)
        {
            if (card.Cost == 0)
            {
                if (lastBet < this.Money * 0.5)
                {
                    int newBet = (int)(lastBet + lastBet * 0.1);
                    return newBet > lastBet ? newBet : newBet + 1;
                }
            }
            else if (lastBet < card.Cost * 1.5)
            {
                int newBet = (int)(lastBet + lastBet * 0.1);
                return newBet > lastBet ? newBet : newBet+1;
            }
            return 0;
        }
    }
}
