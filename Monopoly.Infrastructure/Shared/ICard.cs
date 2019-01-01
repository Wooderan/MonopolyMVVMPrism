using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Infrastructure.Shared
{

    public enum CardType
    {
        TOWN,
        STATION,
        ACTION,
    }

    public interface ICard
    {
        CardType Type { get; }
        string Name { get; }
        int Cost { get; }
        int PledgeCost { get; }
        int Houses { get; }
        int HouseCost { get; }
        ICardGroup CardGroup { get; }
        ITaxGroup TaxGroup { get; }
        IPlayer Owner { get; set; }
        int CurrentTax { get; }

        void AddHouse();
        void RemoveHouse();
    }
}
