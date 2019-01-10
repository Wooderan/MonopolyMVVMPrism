using Monopoly.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
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
        AbstractPlayer Owner { get; set; }
        int CurrentTax { get; }
    }
}
