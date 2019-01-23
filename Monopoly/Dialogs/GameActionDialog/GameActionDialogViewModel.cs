using Monopoly.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Dialogs
{
    class GameActionDialogViewModel : BindableBase
    {

        #region Constructors

        public GameActionDialogViewModel(string message, Action closeAction)
        {
            this.Message = message;
            _closeAction = closeAction;
        }

        #endregion

        #region Commands

        private DelegateCommand _acceptCommand;
        public DelegateCommand AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = new DelegateCommand(ExecuteAcceptCommand));

        void ExecuteAcceptCommand()
        {
            _closeAction?.Invoke();
        }

        #endregion

        #region Properties

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion

        #region Fields

        private Action _closeAction;

        #endregion

    }
}
