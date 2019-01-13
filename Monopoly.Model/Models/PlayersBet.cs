using Monopoly.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class PlayersBet
    {
        public AbstractPlayer Player { get; private set; }
        public int Bet { get; private set; }

        public PlayersBet(AbstractPlayer player, int bet)
        {
            Player = player;
            Bet = bet;
        }
    }
}
