using Monopoly.Model.Events;
using Monopoly.Model.Interfaces;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class TestDiceProvider : BindableBase, IDiceProvider
    {
        private int _step = 0;

        private int _leftCube;
        public int LeftCube
        {
            get { return _leftCube; }
            set { SetProperty(ref _leftCube, value); }
        }

        private int _rightCube;
        public int RightCube
        {
            get { return _rightCube; }
            set { SetProperty(ref _rightCube, value); }
        }

        private int _result;
        public int Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        public void RollDice()
        {
            Random rand = new Random();
            this.LeftCube = rand.Next(1, 6);
            this.RightCube = rand.Next(1, 6);
            switch (_step)
            {
                case 0:
                    LeftCube = RightCube;
                    this.Result = 3;
                    break;
                case 1:
                    this.Result = 27;
                    break;
                case 2:
                    this.Result = 9;
                    break;
                //case 3:
                //    this.Result = 5;
                //    break;
                //case 6:
                //    this.Result = 10;
                //    break;
                //case 11:
                //    this.Result = 10;
                //    break;
                default:
                    this.Result = this.LeftCube + this.RightCube;
                    break;
            }
            _step++;
        }
    }
}
