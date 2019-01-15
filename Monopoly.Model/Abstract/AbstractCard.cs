using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System;

namespace Monopoly.Model.Abstract
{

    public abstract class AbstractCard : BindableBase, ICard
    {

        #region Fields

        protected CardType _type;
        protected string _name;

        public CardType Type { get => _type; protected set => _type = value; }
        public string Name { get => _name; protected set => _name = value; }

        #endregion // Fields
    }
}
