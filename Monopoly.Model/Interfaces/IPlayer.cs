using Monopoly.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Monopoly.Model.Interfaces
{
    public interface IPlayer
    {

        #region Events

        event Action<int> MoneyDecreaseEvent;
        event Action<int> MoneyIncreaseEvent;

        #endregion

        string Nickname { get; set; }
        string Avatar { get; set; }
        string Chip { get; set; }
        int Money { get; set; }
        ObservableCollection<AbstractCard> RealtyCards { get; set; }
        ObservableCollection<AbstractCard> ActionCards { get; set; }
        int CardPosition { get; }
        bool HaveMoney { get; }
        bool IsActive { get; }
    }
}
