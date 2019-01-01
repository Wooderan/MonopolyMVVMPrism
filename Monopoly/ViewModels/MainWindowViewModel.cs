using Monopoly.Model.Interfaces;
using Prism.Mvvm;

namespace Monopoly.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Constructor
        public MainWindowViewModel(IGameManager gameManager)
        {
            this.GameManager = gameManager;
        }
        #endregion

        #region Fields
        public  IGameManager GameManager { get; private set; }
        #endregion
    }
}
