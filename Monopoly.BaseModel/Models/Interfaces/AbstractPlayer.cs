using Monopoly.BaseModel.Images;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Monopoly.BaseModel.Models.Interfaces
{
    public class AbstractPlayer : BindableBase
    {
        #region Fields

        private string _nickname;
        private BitmapImage _avatar;
        private BitmapImage _chip;
        private int _money;

        public string Nickname { get => _nickname; set => _nickname = value; }
        public BitmapImage Avatar {
            get
            {
                if (_avatar == null)
                {
                    _avatar = DefaultImagesLocator.GetDefaultAvatar();
                }
                return _avatar;
            }
            set => _avatar = value;
        }
        public BitmapImage Chip {
            get
            {
                if (_chip == null)
                {
                    _chip = DefaultImagesLocator.GetDefaultChip();
                }
                return _chip;
            }
            set => _chip = value;
        }

        //bindable
        public int Money { get => _money; set => this.SetProperty(ref _money, value); }
        public ObservableCollection<AbstractCard> RealtyCards { get; set; }
        public ObservableCollection<AbstractCard> ActionCards { get; set; }

        #endregion

    }
}
