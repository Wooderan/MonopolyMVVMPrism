using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface IDiceProvider
    {
        void RollDice();

        int LeftCube { get; }
        int RightCube { get; }
        int Result { get; }
    }
}
