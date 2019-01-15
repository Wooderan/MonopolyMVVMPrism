using Monopoly.Model.Abstract;
using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    class EventCard : AbstractCard
    {
        public EventCard(string name, string eventPicture, Action<IGameManager> eventAction)
        {
            this.Type = CardType.EVENT;
            this.Name = name;
            this.EventPicture = eventPicture;
            this.EventAction = eventAction;
        }

        #region Properties

        private string _eventPicture;
        public string EventPicture {
            get
            {
                if (_eventPicture == null)
                {
                    _eventPicture = DefaultImagesLocator.GetDefaultEventPicture();
                }
                return _eventPicture;
            }
            private set => _eventPicture = value;
        }

        public Action<IGameManager> EventAction { get; private set; }

        #endregion
    }
}
