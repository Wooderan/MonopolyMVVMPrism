using Monopoly.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface IExchangeManager
    {
        #region Methods

        void SetPlayers(AbstractPlayer playerLeft, AbstractPlayer playerRight);
        void Exchange();

        #endregion

        #region Properties

        ObservableCollection<AbstractCard> FromPlayerLeftCards { get; }
        ObservableCollection<AbstractCard> FromPlayerRightCards { get; }

        int FromPlayerLeftMoney { get; set; }
        int FromPlayerRightMoney { get; set; }

        AbstractPlayer PlayerLeft { get; }
        AbstractPlayer PlayerRight { get; }

        #endregion
    }
}
