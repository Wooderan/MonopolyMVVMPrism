using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.GameField.ViewModels
{
    class FieldViewModel : BindableBase
    {

        public FieldViewModel(IRegionManager regionManager)
        {
            this.Message = "Hello from view model!";

            this.GameManager = (IGameManager)regionManager.Regions["GameFieldRegion"].Context;

        }

        private IGameManager _gameManager;
        public IGameManager GameManager
        {
            get { return _gameManager; }
            set { SetProperty(ref _gameManager, value); }
        }

        private string _message = "Hello from view model!";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ReadOnlyObservableCollection<AbstractCard> Cards => this.GameManager?.Cards ?? null;

    }
}
