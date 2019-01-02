using Monopoly.Model.Abstract;
using System.Windows.Media.Imaging;

namespace Monopoly.Model.Models
{
    class LocalPlayer : AbstractPlayer
    {
        public LocalPlayer(string nickname, string avatar, string chip, int money)
        {
            this.Nickname = nickname;
            this.Avatar = avatar;
            this.Chip = chip;
            this.Money = money;
        }
    }
}
