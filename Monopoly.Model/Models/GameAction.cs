using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class GameAction
    {
        public GameAction(string message, Action<IGameManager> action)
        {
            Message = message;
            Action = action;
        }

        public string Message { get; set; }
        public Action<IGameManager> Action { get; set; }
    }
}
