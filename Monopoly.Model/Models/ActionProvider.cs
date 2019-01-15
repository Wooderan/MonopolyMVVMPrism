using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class ActionProvider : IActionProvider
    {
        #region Constructor

        public ActionProvider()
        {
            _chestActions = new List<GameAction>
            {
                new GameAction("You found a treasure", (gm) => gm.GiveMoney(200))
            };

            _luckyActions = new List<GameAction>
            {
                new GameAction("It's mock lucky action", (gm) => { })
            };
        }

        #endregion

        #region Methods

        public GameAction GetRandomChestAction()
        {
            return this.GetRandomFromList(_chestActions);
        }

        public GameAction GetRandomLuckyAction()
        {
            return this.GetRandomFromList(_luckyActions);
        }

        private GameAction GetRandomFromList(List<GameAction> list)
        {
            Random rand = new Random();
            int i = rand.Next(0, list.Count-1);
            return list[i];
        }

        #endregion

        #region Fields

        private List<GameAction> _chestActions;
        private List<GameAction> _luckyActions;

        #endregion

    }
}
