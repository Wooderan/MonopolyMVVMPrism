using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monopoly.Model.Interfaces
{
    public interface ICardGroup
    {
        List<ICard> Cards { get; set; }
        Color GroupColor { get; set; }
    }
}
