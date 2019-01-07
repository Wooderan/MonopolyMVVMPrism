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

        public BuyOrAuctionDialogViewModel(Action<DialogResults?> closeAction)
        {
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


        #endregion

    }
}
