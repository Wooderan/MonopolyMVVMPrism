using Monopoly.Model.Abstract;
using Monopoly.Model.Model;
using Monopoly.Model.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Events
{
    public class ShowPlayerDetailEvent : PubSubEvent<AbstractPlayer>{}
    public class ShowAvailableForBuildingTowns : PubSubEvent{}
    public class ShowAvailableForDestroyingTowns : PubSubEvent{}
    public class ShowAvailableForMortgageTowns : PubSubEvent{}
    public class ShowAvailableForBuyFromMortgageTowns : PubSubEvent{}
    public class StopShowAvailableForDestroyingTowns : PubSubEvent{}
    public class StopShowAvailableForBuildingTowns : PubSubEvent{}
    public class StopShowAvailableForMortgageTowns : PubSubEvent{}
    public class StopShowAvailableForBuyFromMortgageTowns : PubSubEvent{}
    public class TradeEvent : PubSubEvent{}
    public class GameActionEvent : PubSubEvent<GameAction>{}
    public class ShowDiceRollingEvent : PubSubEvent<Action>{}
    public class JailDialogEvent : PubSubEvent<AbstractPlayer>{}
    public class ShowAvatarsEvent : PubSubEvent<PlayerParameters>{}
    public class ShowChipsEvent : PubSubEvent<PlayerParameters>{}
}
