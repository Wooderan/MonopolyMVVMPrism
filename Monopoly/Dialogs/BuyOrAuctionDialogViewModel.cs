using Monopoly.Model.Abstract;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Dialogs
{
    class BuyOrAuctionDialogViewModel : BindableBase
    {

        #region Constructors

        public BuyOrAuctionDialogViewModel(AbstractCard card, Action<DialogResults?> closeAction)
        {
            this.Card = card;
            this.CloseCommand = new DelegateCommand<DialogResults?>((dr) => closeAction(dr));
            this.Title = "Buy or Auction";
        }

        #endregion

        #region Commands

        private DelegateCommand<DialogResults?> _closeCommand;
        public DelegateCommand<DialogResults?> CloseCommand { get; private set; }


        #endregion

        #region Fields

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private AbstractCard _card;
        public AbstractCard Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }


        #endregion

    }
}
