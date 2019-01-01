using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Infrastructure.Shared
{
    public interface IGameManager
    {
        //ObservableCollection<IPlayer> Players { get; set; }
        ReadOnlyObservableCollection<ICard> Cards { get;}

        //IPlayer CurrentPlayer { get; }
    }
}
