using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Monopoly.Dialogs
{
    class ShowDiceRollingDialogViewModel : BindableBase
    {

        #region Constructors

        public ShowDiceRollingDialogViewModel(int left, int right, Action closeAction)
        {
            _left = left;
            _right = right;
            _closeAction = closeAction;
            Task.Factory.StartNew(this.RollDice);
        }

        #endregion

        #region Methods

        private void RollDice()
        {
            Random rand = new Random();

            for (int i = 0; i < 15; i++)
            {
                this.HideAll();
                this.ShowLeft(rand.Next(1, 6));
                this.ShowRight(rand.Next(1, 6));
                Thread.Sleep(150);
            }

            this.HideAll();
            this.ShowLeft(_left);
            this.ShowRight(_right);
            Thread.Sleep(1000);
            _closeAction?.Invoke();

        }

        private void HideAll()
        {
            this.Left1 = false;
            this.Left2 = false;
            this.Left3 = false;
            this.Left4 = false;
            this.Left5 = false;
            this.Left6 = false;
            this.Right1 = false;
            this.Right2 = false;
            this.Right3 = false;
            this.Right4 = false;
            this.Right5 = false;
            this.Right6 = false;
        }

        private void ShowLeft(int i)
        {
            switch (i)
            {
                case 1:
                    this.Left1 = true;
                    break;
                case 2:
                    this.Left2 = true;
                    break;
                case 3:
                    this.Left3 = true;
                    break;
                case 4:
                    this.Left4 = true;
                    break;
                case 5:
                    this.Left5 = true;
                    break;
                case 6:
                    this.Left6 = true;
                    break;
                default:
                    break;
            }
        }

        private void ShowRight(int i)
        {
            switch (i)
            {
                case 1:
                    this.Right1 = true;
                    break;
                case 2:
                    this.Right2 = true;
                    break;
                case 3:
                    this.Right3 = true;
                    break;
                case 4:
                    this.Right4 = true;
                    break;
                case 5:
                    this.Right5 = true;
                    break;
                case 6:
                    this.Right6 = true;
                    break;
                default:
                    break;
            }
        }

        #endregion

        //#region Commands

        //private DelegateCommand _acceptCommand;
        //public DelegateCommand AcceptCommand =>
        //    _acceptCommand ?? (_acceptCommand = new DelegateCommand(ExecuteAcceptCommand));

        //void ExecuteAcceptCommand()
        //{
        //    _closeAction?.Invoke();
        //}

        //#endregion

        #region Properties

        private bool _left1;
        public bool Left1
        {
            get { return _left1; }
            set { SetProperty(ref _left1, value); }
        }
        private bool _left2;
        public bool Left2
        {
            get { return _left2; }
            set { SetProperty(ref _left2, value); }
        }
        private bool _left3;
        public bool Left3
        {
            get { return _left3; }
            set { SetProperty(ref _left3, value); }
        }
        private bool _left4;
        public bool Left4
        {
            get { return _left4; }
            set { SetProperty(ref _left4, value); }
        }
        private bool _left5;
        public bool Left5
        {
            get { return _left5; }
            set { SetProperty(ref _left5, value); }
        }
        private bool _left6;
        public bool Left6
        {
            get { return _left6; }
            set { SetProperty(ref _left6, value); }
        }
        private bool _right1;
        public bool Right1
        {
            get { return _right1; }
            set { SetProperty(ref _right1, value); }
        }
        private bool _right2;
        public bool Right2
        {
            get { return _right2; }
            set { SetProperty(ref _right2, value); }
        }
        private bool _right3;
        public bool Right3
        {
            get { return _right3; }
            set { SetProperty(ref _right3, value); }
        }
        private bool _right4;
        public bool Right4
        {
            get { return _right4; }
            set { SetProperty(ref _right4, value); }
        }
        private bool _right5;
        public bool Right5
        {
            get { return _right5; }
            set { SetProperty(ref _right5, value); }
        }
        private bool _right6;
        public bool Right6
        {
            get { return _right6; }
            set { SetProperty(ref _right6, value); }
        }


        public ObservableCollection<bool> LeftVisibilities = new ObservableCollection<bool>() { false,false,false, false, false, false };
        public ObservableCollection<bool> RightVisibilities = new ObservableCollection<bool>() { false, false, false, false, false, false };

        #endregion

        #region Fields

        private Action _closeAction;
        private int _left;
        private int _right;

        #endregion

    }
}
