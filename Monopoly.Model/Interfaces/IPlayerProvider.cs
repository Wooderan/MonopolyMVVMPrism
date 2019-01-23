using Monopoly.Model.Abstract;
using System.Collections.ObjectModel;

namespace Monopoly.Model.Interfaces
{
    public interface IPlayerProvider
    {
        ObservableCollection<AbstractPlayer> GetPlayers();
        void UpdatePlayers(ObservableCollection<AbstractPlayer> newPlayers);
    }
}
