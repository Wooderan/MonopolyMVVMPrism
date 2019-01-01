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
        string Nickname { get; set; }
        BitmapImage Avatar { get; set; }
        BitmapImage Chip { get; set; }
        int Money { get; set; }
        ObservableCollection<ICard> RealtyCards { get; set; }
        ObservableCollection<ICard> ActionCards { get; set; }
    }
}
