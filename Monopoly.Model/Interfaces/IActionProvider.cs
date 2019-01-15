using Monopoly.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Interfaces
{
    public interface IActionProvider
    {
        GameAction GetRandomChestAction();
        GameAction GetRandomLuckyAction();
    }
}
