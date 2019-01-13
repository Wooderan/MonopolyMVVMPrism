using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class TestDiceProvider : IDiceProvider
    {
        private int _step = 0;

        public bool RollDice(out int result)
        {
            switch (_step)
            {
                case 0:
                    result = 3;
                    break;
                case 1:
                    result = 3;
                    break;
                case 2:
                    result = 5;
                    break;
                case 3:
                    result = 5;
                    break;
                default:
                    Random rand1 = new Random();
                    Random rand2 = new Random();
                    result = rand1.Next(1, 6) + rand2.Next(1, 6);
                    break;
            }
            _step++;
            return false;
        }
    }
}
