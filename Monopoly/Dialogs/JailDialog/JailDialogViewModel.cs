using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Dialogs
{
    class JailDialogViewModel : BindableBase
    {

        #region Constructors

        public JailDialogViewModel(bool haveActionCard, Action<JailDialogResults?> closeAction)
        {
            this.HaveActionCard = haveActionCard;
            _closeAction = closeAction;
        }

        #endregion

        #region Commands

        private DelegateCommand<JailDialogResults?> _acceptCommand;
        public DelegateCommand<JailDialogResults?> AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = new DelegateCommand<JailDialogResults?>(ExecuteAcceptCommand));

        void ExecuteAcceptCommand(JailDialogResults? result)
        {
            _closeAction?.Invoke(result);
        }

        #endregion

        #region Properties

        private bool _haveActionCard;
        public bool HaveActionCard
        {
            get { return _haveActionCard; }
            set { SetProperty(ref _haveActionCard, value); }
        }

        #endregion

        #region Fields

        private Action<JailDialogResults?> _closeAction;

        #endregion

    }
}
