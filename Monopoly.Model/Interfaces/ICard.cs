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
        EVENT,
    }

    public interface ICard
    {
        CardType Type { get; }
        string Name { get; }
    }
}
