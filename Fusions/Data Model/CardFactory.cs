using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusions.DataModel
{
    public static class CardFactory
    {
        public static Card CreateCard(string name, CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Base:
                case CardType.FusionResult:
                    return new Monster(name, cardType);
                case CardType.Equipment:
                    return new Equipment(name);
                case CardType.Magic:
                    return new Magic(name);
                case CardType.Trap:
                    return new Trap(name);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
