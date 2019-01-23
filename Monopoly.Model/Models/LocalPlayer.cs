using Monopoly.Model.Abstract;
using System.Windows.Media.Imaging;

namespace Monopoly.Model.Models
{
    public class LocalPlayer : AbstractPlayer
    {
        public LocalPlayer(string nickname, string avatar, string chip, int money) : base()
        {
            this.Nickname = nickname;
            this.Avatar = avatar;
            this.Chip = chip;
            this.Money = money;
        }
    }
}
