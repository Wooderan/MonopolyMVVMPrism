using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
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

        public FieldViewModel(ICardLocator cardLocator)
        {
            this.Message = "Hello from view model!";
            this.Cards = cardLocator.GetCardSet();
        }

        private string _message = "Hello from view model!";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private ObservableCollection<AbstractCard> _cards;
        public ObservableCollection<AbstractCard> Cards
        {
            get { return _cards; }
            set { SetProperty(ref _cards, value); }
        }

    }
}
