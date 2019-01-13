using Monopoly.Model.Abstract;
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
}
