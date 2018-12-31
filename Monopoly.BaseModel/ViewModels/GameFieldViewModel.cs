using Monopoly.BaseModel.Models.Interfaces;
using Monopoly.BaseModel.Services;
using Prism.Ioc;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Monopoly.BaseModel.ViewModels
{
    public class GameFieldViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public GameFieldViewModel()
        {
            Message = "GameField";
            this.Cards = CardLocator.GetStandartCardSet();
        }

        private ObservableCollection<AbstractCard> _cards;
        public ObservableCollection<AbstractCard> Cards
        {
            get { return _cards; }
            set { SetProperty(ref _cards, value); }
        }
    }
}
