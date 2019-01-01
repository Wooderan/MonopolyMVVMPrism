using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Monopoly.BaseModel.Models.Interfaces
{
    public class AbstractPlayer : BindableBase, IPlayer
    {
        #region Fields

        protected string _nickname;
        protected BitmapImage _avatar;
        protected BitmapImage _chip;
        protected int _money;

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
        public ObservableCollection<ICard> RealtyCards { get; set; }
        public ObservableCollection<ICard> ActionCards { get; set; }

        #endregion

    }
}
