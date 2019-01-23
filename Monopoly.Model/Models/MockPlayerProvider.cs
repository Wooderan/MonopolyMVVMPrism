using Monopoly.Model.Abstract;
using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class MockPlayerProvider : IPlayerProvider
    {
        private ObservableCollection<AbstractPlayer> Players;

        public ObservableCollection<AbstractPlayer> GetPlayers()
        {
            if (Players == null)
            {
                return new ObservableCollection<AbstractPlayer>()
                {
                    new LocalPlayer("Player", DefaultImagesLocator.GetAvatar("man-1.png"), DefaultImagesLocator.GetChip("coffe-cup-aqua.png"), 2500),
                    new ComputerPlayer("Computer", DefaultImagesLocator.GetAvatar("man-2.png"), DefaultImagesLocator.GetChip("coffe-cup-blue.png"), 2500),
                };
            }
            else
            {
                return Players;
            }
        }

        public void UpdatePlayers(ObservableCollection<AbstractPlayer> newPlayers)
        {
            Players = newPlayers;
        }
    }
}
