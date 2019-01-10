using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface IDiceProvider
    {
        bool RollDice(out int result);
    }
}
