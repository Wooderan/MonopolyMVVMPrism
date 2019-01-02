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
        public ObservableCollection<AbstractPlayer> GetPlayers()
        {
            return new ObservableCollection<AbstractPlayer>()
            {
                new LocalPlayer("Player1", DefaultImagesLocator.GetAvatar("man-1.png"), DefaultImagesLocator.GetChip("coffe-cup-aqua.png"), 2500),
                new LocalPlayer("Player2", DefaultImagesLocator.GetAvatar("man-2.png"), DefaultImagesLocator.GetChip("coffe-cup-blue.png"), 2500),
                new LocalPlayer("Player3", DefaultImagesLocator.GetAvatar("man-3.png"), DefaultImagesLocator.GetChip("coffe-cup-green.png"), 2500),

            };
        }
    }
}
