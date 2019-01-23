using Monopoly.Model.Abstract;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Model
{
   
    public class PlayerParameters : BindableBase
    {
        private PlayerType _type;
        public PlayerType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set { SetProperty(ref _nickname, value); }
        }
        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set { SetProperty(ref _avatar, value); }
        }
        private string _chip;
        public string Chip
        {
            get { return _chip; }
            set { SetProperty(ref _chip, value); }
        }

        public PlayerParameters()
        {
        }

        public PlayerParameters(PlayerType type, string nickname, string avatar, string chip)
        {
            Type = type;
            Nickname = nickname;
            Avatar = avatar;
            Chip = chip;
        }
    }
}
