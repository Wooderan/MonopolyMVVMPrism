using Monopoly.Model.Abstract;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface IGameManager
    {
        //ObservableCollection<IPlayer> Players { get; set; }
        ReadOnlyObservableCollection<AbstractCard> Cards { get;}
        ObservableCollection<AbstractPlayer> Players { get; }

        //IPlayer CurrentPlayer { get; }
    }
}
