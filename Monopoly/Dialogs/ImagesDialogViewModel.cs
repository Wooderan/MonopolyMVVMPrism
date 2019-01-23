using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Dialogs
{
    class ImagesDialogViewModel : BindableBase
    {
        #region Constructors

        public ImagesDialogViewModel(List<string> images, Action<string> closeAction)
        {
            this.Images = new ObservableCollection<string>();
            foreach (string image in images)
            {
                this.Images.Add(image);
            }
            _closeAction = closeAction;
        }

        #endregion

        #region Commands

        private DelegateCommand<string> _acceptCommand;
        public DelegateCommand<string> AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = new DelegateCommand<string>(ExecuteAcceptCommand));

        void ExecuteAcceptCommand(string image)
        {
            _closeAction?.Invoke(image);
        }

        #endregion

        #region Properties

        private ObservableCollection<string> _images;
        public ObservableCollection<string> Images
        {
            get { return _images; }
            set { SetProperty(ref _images, value); }
        }

        #endregion

        #region Fields

        private Action<string> _closeAction;

        #endregion

    }
}
